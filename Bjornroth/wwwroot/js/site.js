function ActivateEventListeners() {
    for (let i = 0; i < 3; i++) {
        document.getElementById(`like-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
                <%#Bjornroth.CmdbRepository.UpdateRating(document.getElementById(`imdbId${i}`).ToString(), document.getElementById(`newRating${i}`).ToString) %>
                UpdateRatingInView(i, "like")
        })
    }

    for (let i = 0; i < 3; i++) {
        document.getElementById(`dislike-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
                <%#Bjornroth.CmdbRepository.UpdateRating(document.getElementById(`imdbId${i}`).ToString(), document.getElementById(`rating${i}`).ToString) %>
                UpdateRatingInView(i, "dislike")
        })
    }
}


function UpdateRatingInView(index, typeOfRating) {
    if (typeOfRating === "dislike") {
        let rating = parseNumber(document.getElementById(`dislike-number${index}`).innerHTML)
        let newRating = rating + 1
        document.getElementById(`dislike-number${index}`).innerHTML = newRating
    }
    else {
        let rating = parseNumber(document.getElementById(`like-number${index}`).innerHTML)
        let newRating = rating + 1
        document.getElementById(`like-number${index}`).innerHTML = newRating
    }
}

window.onload = ActivateEventListeners();

alert(window.onload)