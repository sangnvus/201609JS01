app.controller("ThreadDetailController", function ($scope, $http, $routeParams, $sce, $location, $rootScope, SweetAlert) {
    $scope.CurrentPath = $location.absUrl();
    $scope.NumberOfComment = 0;
    var threadId = $routeParams.Id;
    var emptySubComment = new Array();
    $scope.SubCommentThread = emptySubComment;
    $scope.isLikeStyle =
         {
             "color": "gray"
         }
    //Flag to change color like button
    var flag = false;
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
    if ($rootScope.User_Information.IsAuthen == true)
    {
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
    }
    //AddLike When Click
   $scope.doLike = function ()
   {
       flag = !flag;
       if (flag == true)
           $scope.isLikeStyle ={"color": "rgb(224, 95, 3)"}
       else $scope.isLikeStyle = { "color": "gray" }
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
            $scope.CommentThread = response.Data;
            $scope.NumberOfComment = $scope.CommentThread.length;
    });
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
                $scope.CommentThread = response.Data;
                $scope.NumberOfComment = $scope.CommentThread.length;
                pageShow = 4;
                index = 2;
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
            $scope.CommentThread[index].NumberSubComment++;
            $(".subContent").val('');

        });
    }
    //Count like for comment
    function countLikeForComment(commentId, index) {
        $http({
            url: "/api/Thread/CountLikeInCommentThread",
            method: "GET",
            params: { commentId: commentId },
            contentType: "application/json",
        }).success(function (response) {
            $scope.CommentThread[index].NumberOfLikes = response.Data;
        });
    }
    //Like Comment
    $scope.likeComment = function (commentId, index) {
        $http({
            url: "/api/Thread/ChangeLikeStateForComment",
            method: "get",
            params: { commentId: commentId },
            contentType: "application/json",
        }).success(function (response) {
            countLikeForComment(commentId, index);

        });
    }
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
       //Delete Comment
     $scope.deleteComment = function (CommentId) {
        $http({
            url: "/api/Thread/DeleteComment",
            method: "GET",
            params: { commentId: CommentId },
            contentType: "application/json",
        }).success(function (response) {
            //Reload Comment
            $http({
                url: "/api/Thread/GetAllComment",
                method: "GET",
                params: { threadId: threadId },
                contentType: "application/json",
            }).success(function (response) {
                $scope.CommentThread = response.Data;
                if (response.Data == null) $scope.NumberOfComment = 0;
                $scope.NumberOfComment = $scope.CommentThread.length;
            });
        });
     };
    //Delete SubComment
     $scope.deleteSubComment = function (SubCommentId, Id, index) {
         $http({
             url: "/api/Thread/DeleteSubComment",
             method: "GET",
             params: { subCommentId: SubCommentId },
             contentType: "application/json",
         }).success(function (response) {
             LoadSubComment(Id, index);
             $scope.CommentThread[index].NumberSubComment--;
         });
     };
    //Load report to view
     $scope.getReportContent = function () {
         $http({
             url: "/api/Report/GetReportContentForThread",
             method: "GET",
             contentType: "application/json",
         }).success(function (response) {
             $scope.ReportContent = response.Data;
         });
     };
     if ($rootScope.User_Information.IsAuthen == true) {
         $http({
             url: "/api/Report/CheckCurrentUserReportedOrNot",
             method: "GET",
             params: { Type: "Threads", ReportTo: threadId },
             contentType: "application/json",
         }).success(function (response) {
             $scope.checkReported = response.Data;
         });
     }
     $scope.setValue = function (setValue) {
         $scope.radioValue = setValue;
     }
    //Send report
     $scope.sendReport = function () {
         $http({
             url: "/api/Report/ReportThread",
             method: "get",
             params: { toThreadId: threadId, Content: $scope.ReportContent[$scope.radioValue] },
             contentType: "application/json",
         }).success(function (response) {
             $(".modal-body").hide();
             $(".sendMessage").hide();
             $(".modal-message").show();
             $(".closeForm").show();
         });
     };

    $scope.reloadComment = function () {
        $http({
            url: "/api/Thread/GetAllComment",
            method: "GET",
            params: { threadId: threadId },
            contentType: "application/json",
        }).success(function (response) {
                $scope.CommentThread = response.Data;
                pageShow = 4;
                index = 2;
        });
    };
     // Handle Loadmore comment
    var pageShow = 4;
    var index = 2;
    $scope.paginationLimit = function (data) {
        return pageShow * index;
    };
    $scope.hasMoreItemsToShow = function () {
        if ($scope.CommentThread != null) {
            if (typeof $scope.CommentThread == "undefined") return false;
            else return pageShow < ($scope.CommentThread.length / index);
        }
        else return false;
    };
    $scope.showMoreItems = function () {
        index = index + 1;
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