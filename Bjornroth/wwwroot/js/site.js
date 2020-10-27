function someTest(imdbId, newRating) {
    const baseUrl = "https://localhost:44313/api/"
    console.log(imdbId)
    try {
        const apiCall = new XMLHttpRequest();
        apiCall.addEventListener("load", reqListener);
        apiCall.open("GET", `${baseUrl}movie/${imdbId}/${newRating}`);
        apiCall.send();
        console.log(apiCall.status)
    }
    catch (error) {
        console.log(error.message)
    }
}


async function testing(imdbId, newRating) {
    const baseUrl = "https://localhost:44313/api/"
    console.log(imdbId)
    try {
        await fetch(`${baseUrl}movie/${imdbId}/${newRating}`, {
            method: 'GET',
            mode: 'no-cors'
        })
    }
    catch (e) {
        console.log(e.message)
    }

}

let numberOfReactButtons = document.getElementsByClassName("submitBtn").length / 2

function activateEventListeners() {
    for (let i = 0; i < numberOfReactButtons; i++) {
        document.getElementById(`like-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
            testing(document.getElementById(`imdbId${i}`).value, "like")
                UpdateRatingInView(i, "like")
        })
    }

    for (let i = 0; i < numberOfReactButtons; i++) {
        document.getElementById(`dislike-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
            testing(document.getElementById(`imdbId${i}`).value, "dislike")
                UpdateRatingInView(i, "dislike")
        })
    }
}

function UpdateRatingInView(index, typeOfRating) {
    if (typeOfRating === "dislike") {
        let rating = parseInt(document.getElementById(`dislike-number${index}`).innerHTML)
        let newRating = rating+1
        document.getElementById(`dislike-number${index}`).innerHTML = newRating
    }
    else {
        let rating = parseInt(document.getElementById(`like-number${index}`).innerHTML)
        let newRating = rating+1
        document.getElementById(`like-number${index}`).innerHTML = newRating
    }
}

alert('hej')
activateEventListeners()