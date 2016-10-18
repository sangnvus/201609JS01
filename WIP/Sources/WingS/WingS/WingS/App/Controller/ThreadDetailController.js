app.controller("ThreadDetailController", function ($scope, $http, $routeParams, $sce, $location, $rootScope) {
    $scope.CurrentPath = $location.absUrl();
    var threadId = $routeParams.Id;
    var emptySubComment = new Array();
    $scope.SubCommentThread = emptySubComment;
    $scope.isLikeStyle =
         {
             "color": "black"
         }
    //Flag to change color like button
    var flag = false;
    $("#isComment").hide();
    //Load ThreadDetail
    $http({
        url: "/api/Thread/GetThreadById",
        method: "GET",
        params: { id: threadId },
        contentType: "application/json",
    }).success(function(response) {
        $scope.Thread = response.Data;
        $scope.Thread.Content = $sce.trustAsHtml($scope.Thread.Content);
   //Get Created User
   $http({
            url: "/api/Thread/GetUserCreatedThread",
            method: "GET",
            params: { id: $scope.Thread.UserID },
            contentType: "application/json",
        }).success(function (response) {
            $scope.user = response.Data;

        });
    });
    //Get NumberOfLike
    function countLike(){
       $http({
       url: "/api/Thread/CountLikeInThread",
       method: "GET",
       params: { threadId: threadId },
       contentType: "application/json",
       }).success(function (response) {
       $scope.Likes = response.Data;
       });
    }
    countLike();
    // CheckCurrentUserIsLikedOrNot
    $http({
       url: "/api/Thread/CheckCurrentUserIsLikedOrNot",
       method: "GET",
       params: { threadId: threadId },
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
    //AddLike When Click
   $scope.doLike = function ()
   {
       flag = !flag;
       if (flag == true)
           $scope.isLikeStyle ={"color": "rgb(224, 95, 3)"}
       else $scope.isLikeStyle = { "color": "black" }
       $http({
           url: "/api/Thread/ChangeLikeState",
           method: "get",
           params: { threadId: threadId },
           contentType: "application/json",
       }).success(function (response) {
           countLike();
       });
   }
    //Load All Commend to view.
    $http({
        url: "/api/Thread/GetAllComment",
        method: "GET",
        params: { threadId: threadId },
        contentType: "application/json",
    }).success(function (response) {
        if (response.Status == "not-found") {
            $("#isComment").show();
        }
        else
        {
            $scope.CommentThread = response.Data;
        }
    });
    //Check is Existed subcomment in each comment
    $http({
        url: "/api/Thread/CheckExistedSubCommentOrNot",
        method: "GET",
    }).success(function (response) {
        $scope.isSubcommentExisted = response.Data;
    });
    $scope.checkIsExistedSubComment = function (threadId)
    {
        if ($scope.isSubcommentExisted != null) {
            for (var i = 0; i <= $scope.isSubcommentExisted.length ; i++) {
                if ($scope.isSubcommentExisted[i] == threadId) return true;
            }
        }
        return false;
    }
  
    //Load SubCommentIfExisted
    function LoadSubComment(Id, index)
    {
        $http({
            url: "/api/Thread/GetSubCommentByCommentId",
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
    $scope.getSubcomment = function(Id, index)
    {
        LoadSubComment(Id, index);
    }
 
    //Add comment API
    $scope.addContent = function () {
        var newContent = $scope.content;
        if (newContent.length < 10)
        {
            $scope.content = "Hãy nhập hơn 10 kí tự để comment";
            return false;
        } else if ($scope.content =="Hãy nhập hơn 10 kí tự để comment")
        {
            $scope.content = "";
            return false;
        }
        else{
        $http({
            url: "/api/Thread/AddComment",
            method: "post",
            data: { ThreadId: threadId, CommentContent: newContent },
            contentType: "application/json",
        }).success(function (response) {
            $scope.content = "";
            //Reload Comment
            $http({
                url: "/api/Thread/GetAllComment",
                method: "GET",
                params: { threadId: threadId },
                contentType: "application/json",
            }).success(function (response) {
                $("#isComment").hide();
                $scope.CommentThread = response.Data;
            });
        });
        }
    };
    //Add subcomment if existed
    $scope.addSubComment = function (xSubcontent,Id, index)
    {
        var subContent = xSubcontent;
        var CommentId = Id;
        $http({
            url: "/api/Thread/AddSubComment",
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
        if (typeof $scope.CommentThread == "undefined") return false;
        else return pageShow < ($scope.CommentThread.length / index);
    };
    $scope.showMoreItems = function () {
        index = index + 1;
    };
    $scope.reloadComment = function () {
        $http({
            url: "/api/Thread/GetAllComment",
            method: "GET",
            params: { threadId: threadId },
            contentType: "application/json",
        }).success(function (response) {
            if (response.Status == "not-found") {
                $("#isComment").show();
            }
            else {
                $scope.CommentThread = response.Data;
                pageShow = 4;
                index = 2;

            }
        });
    };
 
});
app.filter('trusted', ['$sce', function ($sce) {
    return function (url) {
        return $sce.trustAsResourceUrl(url);
    };
}]);

app.directive('myEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.myEnter);
                });

                event.preventDefault();
            }
        });
    };
});