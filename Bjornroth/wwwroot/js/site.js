function ActivateEventListeners() {
    for (let i = 0; i < 3; i++) {
        document.getElementById(`like-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
            UpdateRating(i, "like")
        })
    }

    for (let i = 0; i < 3; i++) {
        document.getElementById(`dislike-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
            UpdateRating(i, "dislike")
        })
    }
}


function UpdateRating(index, typeOfRating) {
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

ActivateEventListeners()