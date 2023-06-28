function toggleFavorite(trackId) {
    var favoriteButton = $('#favoriteButton_' + trackId);
    var isFavorite = favoriteButton.hasClass('star');

    if (isFavorite) {
        // Removing the track from favorites
        deleteFavoriteSound(trackId);
    } else {
        // Adding the track to favorites
        addFavoriteSound(trackId);
    }
}

function addFavoriteSound(trackId) {
    $.ajax({
        url: '/FavoriteSounds/AddFavoriteSound',
        type: 'POST',
        data: { soundId: trackId },
        success: function (response) {
            var favoriteButton = $('#favoriteButton_' + trackId);
            favoriteButton.addClass('star');

            // Update the isFavorite property in the corresponding track
            var track = allTracks.find(function (t) {
                return t.id === trackId;
            });
            if (track) {
                track.isFavorite = true;
            }
        },
        error: function (xhr, status, error) {
            console.log('An error occurred while adding the track to favorites');
        }
    });
}

function deleteFavoriteSound(trackId) {
    $.ajax({
        url: '/FavoriteSounds/DeleteFavoriteSound',
        type: 'POST',
        data: { soundId: trackId },
        success: function (response) {
            var favoriteButton = $('#favoriteButton_' + trackId);
            favoriteButton.removeClass('star');

            // Update the isFavorite property in the corresponding track
            var track = allTracks.find(function (t) {
                return t.id === trackId;
            });
            if (track) {
                track.isFavorite = false;
            }
        },
        error: function (xhr, status, error) {
            console.log('An error occurred while removing the track from favorites');
        }
    });
}

function checkFavoriteSound(trackId) {
    $.ajax({
        url: '/FavoriteSounds/IsThisSoundFavorite',
        type: 'POST',
        data: { soundId: trackId },
        success: function (response) {
            var favoriteButton = $('#favoriteButton_' + trackId);

            if (response) {
                favoriteButton.addClass('star');
            } else {
                favoriteButton.removeClass('star');
            }
        },
        error: function (xhr, status, error) {
            console.log('An error occurred while checking if the sound is favorite');
        }
    });
}

function getFavoriteUserCount(soundId) {
    $.ajax({
        url: '/FavoriteSounds/GetCountFavaritsSoundUsers',
        type: 'GET',
        data: { soundId: soundId },
        success: function (response) {
            $('#favoriteUserCount_' + soundId).text(response);
        },
        error: function (xhr, status, error) {
            console.log('An error occurred while retrieving the favorite user count');
        }
    });
}