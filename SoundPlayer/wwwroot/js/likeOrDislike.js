function toggleLike(trackId) {   
    $.ajax({
        url: '/LikeOrDislike/AddLikeOrDeleteOrUpdate',
        type: 'POST',
        data: { soundId: trackId },
        success: function (response) {
            console.log('The like has been toggled successfully');

            var likeButton = $('#likeButton_' + trackId);
            var dislikeButton = $('#dislikeButton_' + trackId);

            if (likeButton.hasClass('active-like')) {
                likeButton.removeClass('active-like');
            } else {
                likeButton.addClass('active-like');

                // Якщо кнопка дизлайку має клас `.active-dislike`, знімайте його
                if (dislikeButton.hasClass('active-dislike')) {
                    dislikeButton.removeClass('active-dislike');
                }
            }

            getLikeCount(trackId);  
            getDislikeCount(trackId);
        },
        error: function (xhr, status, error) {
            console.log('An error occurred while toggling the like');
        }
    });
}

function toggleDislike(trackId) {
    $.ajax({
        url: '/LikeOrDislike/AddDislikeOrDeleteOrUpdate',
        type: 'POST',
        data: { soundId: trackId },
        success: function (response) {
            console.log('The dislike has been toggled successfully');

            var dislikeButton = $('#dislikeButton_' + trackId);
            var likeButton = $('#likeButton_' + trackId);

            if (dislikeButton.hasClass('active-dislike')) {
                dislikeButton.removeClass('active-dislike');
            } else {
                dislikeButton.addClass('active-dislike');

                // Якщо кнопка лайку має клас `.active-like`, знімайте його
                if (likeButton.hasClass('active-like')) {
                    likeButton.removeClass('active-like');
                }
            }

            getDislikeCount(trackId);
            getLikeCount(trackId);   
        },
        error: function (xhr, status, error) {
            console.log('An error occurred while toggling the dislike');
        }
    });
}

function getLikeCount(trackId) {
    $.ajax({
        url: '/LikeOrDislike/GetLikeCount',
        type: 'GET',
        data: { trackId: trackId },
        success: function (response) {
            $('#likeCount_' + trackId).text(response);
        },
        error: function (xhr, status, error) {
            console.log('An error occurred while retrieving the like count');
        }
    });
}

function getDislikeCount(trackId) {
    $.ajax({
        url: '/LikeOrDislike/GetDislikeCount',
        type: 'GET',
        data: { trackId: trackId },
        success: function (response) {
            $('#dislikeCount_' + trackId).text(response);
        },
        error: function (xhr, status, error) {
            console.log('An error occurred while retrieving the dislike count');
        }
    });
}

function checkUserVote(trackId) {
    // Запит на перевірку, чи користувач вже проголосував за трек
    $.ajax({
        url: '/LikeOrDislike/IsUserAlreadyVoted?trackId=' + trackId,
        type: 'GET',
        success: function (response) {
            if (response) {
                // Якщо користувач вже голосував, перевіряємо, чи це лайк чи дизлайк
                isLikeOrDislike(trackId);
            }
        },
        error: function (error) {
            console.log('Помилка перевірки голосу користувача:', error);
        }
    });
}

function isLikeOrDislike(trackId) {
    // Запит на перевірку, чи це лайк чи дизлайк
    $.ajax({
        url: '/LikeOrDislike/IsLikeOrDislike?trackId=' + trackId,
        type: 'GET',
        success: function (response) {
            if (response) {
                // Додати стиль .active-like
                $('#likeButton_' + trackId).addClass('active-like');
            } else {
                // Додати стиль .active-dislike
                $('#dislikeButton_' + trackId).addClass('active-dislike');
            }
        },
        error: function (error) {
            console.log('Помилка перевірки типу голосу:', error);
        }
    });
}



