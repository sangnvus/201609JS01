app.controller("EventDetailController", function ($scope, $http, $routeParams, $sce, $location) {
    var eventId = $routeParams.Id;
    var emptySubComment = new Array();
    $scope.SubCommentEvent = emptySubComment;
    $scope.isLikeStyle =
         {
             "color": "black"
         }
    //Flag to change color like button
    var flag = false;
    //Hide if no comment
    $("#isComment").hide();
    //Load event Detail
    $http({
        url: "/api/Event/GetEventDetailById",
        method: "Get",
        params: { id: eventId },
        contentType: "application/json",
    }).success(function (response) {
        $scope.Event = response.Data;
        $scope.Event.VideoUrl = "http://www.youtube.com/embed/" + getYoutubeId($scope.Event.VideoUrl);
        $scope.Event.ExpectedMoney = $scope.Event.ExpectedMoney.toFixed(1).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        $scope.Event.VideoUrl = $sce.trustAsResourceUrl($scope.Event.VideoUrl);
        $scope.Event.Content = $sce.trustAsHtml($scope.Event.Content);
    });
    // Load TimeLine
    $http({
        url: "/api/Event/GetEventTimeLineByEventId",
        method: "Get",
        params: { id: eventId },
        contentType: "application/json",
    }).success(function (response) {
        $scope.EventTimeLine = response.Data;
    });
    //Get NumberOfLike
    function countLike() {
        $http({
            url: "/api/Event/CountLikeInEvent",
            method: "GET",
            params: { eventId: eventId },
            contentType: "application/json",
        }).success(function (response) {
            $scope.Likes = response.Data;
        });
    }
    countLike();
    //Load All Commend to view.
    $http({
        url: "/api/Event/GetAllComment",
        method: "GET",
        params: { eventId: eventId },
        contentType: "application/json",
    }).success(function (response) {
        if (response.Status == "not-found") {
            $("#isComment").show();
        }
        else {
            $scope.CommentEvent = response.Data;
        }
    });
    //Add comment API
    $scope.addContent = function () {
        var newContent = $scope.content;
        if (newContent.length < 10) {
            $scope.content = "Hãy nhập hơn 10 kí tự để comment";
            return false;
        } else if ($scope.content == "Hãy nhập hơn 10 kí tự để comment") {
            $scope.content = "";
            return false;
        }
        else {
            $http({
                url: "/api/Event/AddComment",
                method: "post",
                data: { ThreadId: eventId, CommentContent: newContent },
                contentType: "application/json",
            }).success(function (response) {
                $scope.content = "";
                //Reload Comment
                $http({
                    url: "/api/Event/GetAllComment",
                    method: "GET",
                    params: { eventId: eventId },
                    contentType: "application/json",
                }).success(function (response) {
                    $("#isComment").hide();
                    $scope.CommentEvent = response.Data;
                });
            });
        }
    };
    //Load SubCommentIfExisted
    function LoadSubComment(Id, index) {
        $http({
            url: "/api/Event/GetSubCommentByCommentId",
            method: "GET",
            params: { CommentId: Id },
            contentType: "application/json",
        }).success(function (response) {
            if (response.Status == "not-found") {
                emptySubComment[index] = null;
            }
            else {
                emptySubComment[index] = response.Data;
            }
        });
    }
    //Call when getSubcomment is Activated
    $scope.getSubcomment = function (Id, index) {
        LoadSubComment(Id, index);
    }
    //Add subcomment 
    $scope.addSubComment = function (xSubcontent, Id, index) {
        var subContent = xSubcontent;
        var CommentId = Id;
        $http({
            url: "/api/Event/AddSubComment",
            method: "post",
            data: { CommentThreadId: CommentId, CommentContent: subContent },
            contentType: "application/json",
        }).success(function (response) {
            //reload all subcommet
            LoadSubComment(Id, index);
            $(".subContent").val('');
        });
    }
    // Handle Loadmore comment
    var pageShow = 4;
    var index = 2;
    $scope.paginationLimit = function (data) {
        return pageShow * index;
    };
    $scope.hasMoreItemsToShow = function () {
        if (typeof $scope.CommentEvent == "undefined") return false;
        else return pageShow < ($scope.CommentEvent.length / index);
    };
    $scope.showMoreItems = function () {
        index = index + 1;
    };
    $scope.reloadComment = function () {
        $http({
            url: "/api/Event/GetAllComment",
            method: "GET",
            params: { eventId: eventId },
            contentType: "application/json",
        }).success(function (response) {
            if (response.Status == "not-found") {
                $("#isComment").show();
            }
            else {
                $scope.CommentEvent = response.Data;
                pageShow = 4;
                index = 2;

            }
        });
    };
    //Send message 
    $scope.sendMessage = function(titleMessage, content)
    {
        $http({
            url: "/api/Message/AddMessage",
            method: "Post",
            data: { Title: titleMessage, Content: content, ReceiverName: $scope.Event.CreatorUserName },
            contentType: "application/json",
        }).success(function (response) {
            $(".modal-body").hide();
            $(".sendMessage").hide();
            $(".modal-message").show();
            $(".closeForm").show();
        });
    }
    $scope.SkipValidation = function(value)
    {
        return $sce.trustAsHtml(value);
    }
    function getYoutubeId(url)
    {
        var regExp = /^.*(youtu\.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*/;
        var match = url.match(regExp);
        if (match && match[2].length == 11) {
            return match[2];
        } else {
            return "";
        }
    }
});