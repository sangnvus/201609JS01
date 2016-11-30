app.controller("EventDetailController", function ($scope, $http, $routeParams, $sce, $location, $window, $rootScope, SweetAlert) {
    //Get Current Location for sharing
    $scope.CurrentPath = $location.absUrl();

    //Get EventId from router to send to backed
    var eventId = $routeParams.Id;
    
    //Set eventId to Window variable to connect chat room
    $window.eventId = eventId;
    //Create SubComment Array
    var emptySubComment = new Array();
    $scope.SubCommentEvent = emptySubComment;

    //Send Number OfComment in each event
    $scope.NumberOfComment = 0;

    //Set LikeStyle when do like or do unlike
    $scope.isLikeStyle =
         {
             "color": "gray"
         }
    //Flag to change color like button
    var flag = false;
    //Load event Detail
    $http({
        url: "/api/Event/GetEventDetailById",
        method: "Get",
        params: { id: eventId },
        contentType: "application/json",
    }).success(function (response) {
        $scope.Event = response.Data;
        $scope.Event.VideoUrl = "http://www.youtube.com/embed/" + getYoutubeId($scope.Event.VideoUrl);
        $scope.Event.PercentMoney = (($scope.Event.RaisedMoney * 100) / $scope.Event.ExpectedMoney).toFixed(1);
        $scope.Event.ExpectedMoney = $scope.Event.ExpectedMoney.toFixed(1).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        $scope.Event.RaisedMoney = $scope.Event.RaisedMoney.toFixed(1).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        $scope.Event.VideoUrl = $sce.trustAsResourceUrl($scope.Event.VideoUrl);
        $scope.Event.Content = $sce.trustAsHtml($scope.Event.Content);
        $(".progressbar").loading($scope.Event.PercentMoney);
    });

    //Load donator
    $http({
        url: "/api/Event/GetDonatorInEvent",
        method: "Get",
        params: { id: eventId },
        contentType: "application/json",
    }).success(function (response) {
        $scope.ListDonator = response.Data;
     
    });
    //Load 3 organization to page
    $http.get("/api/Organization/GetTopThreeOrganization").success(function (response) {
        $scope.Organization = response.Data;
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
    if ($rootScope.User_Information.IsAuthen == true)
    {
    // CheckCurrentUserIsLikedOrNot
    $http({
        url: "/api/Event/CheckCurrentUserIsLikedOrNot",
        method: "GET",
        params: { eventId: eventId },
        contentType: "application/json",
    }).success(function (response) {
        if (response.Data == true) {
            flag = true;
        //Set Color for like button.
            $scope.isLikeStyle =
            {
                "color": "rgb(224, 95, 3)"
            }
    }
    });
    }
  
    //Load All Commend to view.
    $http({
        url: "/api/Event/GetAllComment",
        method: "GET",
        params: { eventId: eventId },
        contentType: "application/json",
    }).success(function (response) {
        if (response.Status == "not-found") {

        }
        else {
            $scope.CommentEvent = response.Data;
            $scope.NumberOfComment = $scope.CommentEvent.length;
           
        }
    });

    //AddLike When Click
    $scope.doLike = function () {
        flag = !flag;
        if (flag == true)
            $scope.isLikeStyle = { "color": "rgb(224, 95, 3)" }
        else $scope.isLikeStyle = { "color": "gray" }
        $http({
            url: "/api/Event/ChangeLikeState",
            method: "get",
            params: { eventId: eventId },
            contentType: "application/json",
        }).success(function (response) {
            countLike();
        });
    }
  

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
                    $scope.CommentEvent = response.Data;
                    $scope.NumberOfComment = $scope.CommentEvent.length;
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
            $scope.CommentEvent[index].NumberSubComment++;
            $(".subContent").val('');
        });
    }
    //
    function countLikeForComment(commentId,index) {
        $http({
            url: "/api/Event/CountLikeInCommentEvent",
            method: "GET",
            params: { commentId: commentId },
            contentType: "application/json",
        }).success(function (response) {
            $scope.CommentEvent[index].NumberOfLikes = response.Data;
        });
    }
    //Like Comment
    $scope.likeComment = function (commentId,index) {
       $http({
            url: "/api/Event/ChangeLikeStateForComment",
            method: "get",
            params: { commentId: commentId },
            contentType: "application/json",
        }).success(function (response) {
            countLikeForComment(commentId,index);
          
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

            }
            else {
                $scope.CommentEvent = response.Data;
                $scope.NumberOfComment = $scope.CommentEvent.length;
                pageShow = 4;
                index = 2;

            }
        });
    };

    //Load List Public message 
        $http({
            url: "/api/Event/GetpublicMessage",
            method: "GET",
            params: { eventId: eventId },
            contentType: "application/json"
        }).success(function (response) {
            $scope.MessageList = response.Data;
        });
    //Delete Comment
     $scope.deleteComment = function (CommentId) {
        $http({
            url: "/api/Event/DeleteComment",
            method: "GET",
            params: { commentId: CommentId },
            contentType: "application/json",
        }).success(function (response) {
            //Reload Comment
            $http({
                url: "/api/Event/GetAllComment",
                method: "GET",
                params: { eventId: eventId },
                contentType: "application/json",
            }).success(function (response) {
                $scope.CommentEvent = response.Data;
                if (response.Data == null) $scope.NumberOfComment = 0;
                $scope.NumberOfComment = $scope.CommentEvent.length;
            });
        });
     };
    // Alert admin before delete comment
     $scope.dialog = function (CommentId) {
        SweetAlert.swal({
            title: "Xóa bình luận",
            text: "Bạn thực sự muốn xóa bình luận này?",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#16a085",
            confirmButtonText: "Có",
            cancelButtonText: "Không",
            closeOnConfirm: false,
            closeOnCancel: true
        },
            function (isConfirm) {
                if (isConfirm) {
                    $scope.deleteComment(CommentId);
                    SweetAlert.swal("Xóa!", "Bạn đã xóa bình luận thành công", "success");
                } else {
                    //do nothing
                }
            });
    };
    //Delete SubComment
     $scope.deleteSubComment = function (SubCommentId, Id, index) {
         $http({
             url: "/api/Event/DeleteSubComment",
             method: "GET",
             params: { subCommentId: SubCommentId },
             contentType: "application/json",
         }).success(function (response) {
             LoadSubComment(Id, index);
             $scope.CommentEvent[index].NumberSubComment--;
         });
     };
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