let numberOfReactButtons

async function testing(imdbId, newRating, index) {
    //const baseUrl = "https://localhost:5001/api/"
    const baseUrl = "https://localhost:44313/api/"
    console.log(imdbId)
    try {
        const api = await fetch(`${baseUrl}movie/${imdbId}/${newRating}`, {
            method: 'GET',
            mode: 'cors'
        })
        if (api.ok) {
            const readableApi = await api.json()

            if (newRating === 'dislike') {
                document.getElementById(`dislike-number${index}`).innerHTML = readableApi.numberOfDislikes
                if (document.getElementById(`nmbr-of-dislikes`)) {
                    document.getElementById(`nmbr-of-dislikes`).innerHTML = readableApi.numberOfDislikes
                }
            }
            else {
                document.getElementById(`like-number${index}`).innerHTML = readableApi.numberOfLikes
                if (document.getElementById(`nmbr-of-likes`)) {
                    document.getElementById(`nmbr-of-likes`).innerHTML = readableApi.numberOfLikes
                }
            }
            return Promise
        }
        else {
            throw api.status
        }
    }
    catch (e) {
        console.log(e.message)
    }
}

function Rating(i) {
    const likes = parseInt(document.getElementById(`like-number${i}`).innerHTML)
    const dislikes = parseInt(document.getElementById(`dislike-number${i}`).innerHTML)
    const ratingPercentage = Math.round(((likes / (likes + dislikes) * 100)))
    const ratingLabel = document.getElementsByClassName("rating-percentage-label")
    if (ratingPercentage) {
        ratingLabel[i].innerHTML = ratingPercentage.toString() + "%"
    }
    else if (likes || dislikes) {
        ratingLabel[i].innerHTML = ratingPercentage.toString() + "%"
    }
    else {
        ratingLabel[i].innerHTML = "N/A"
    }

    if (ratingPercentage > 50) {
        ratingLabel[i].style.color = "green"
    }
    else if (ratingPercentage < 50) {
        ratingLabel[i].style.color = "red"
    }
    else {
        ratingLabel[i].style.color = "yellow"
    }
}

function ChangeContent(rating, i) {
    var form = document.getElementsByClassName("like-buttons-full-movie-detail")

    if (rating == "liked") {
        form[i].innerHTML = "<h1 style='color: green'>LIKED</h1>"
    }

    else {
        form[i].innerHTML = "<h1 style='color: red'>DISLIKED</h1>"
    }

}

async function activateEventListeners() {
    for (let i = 0; i < numberOfReactButtons; i++) {
        document.getElementById(`like-btn${i}`).addEventListener("click", async function (event) {
            event.preventDefault()
            await testing(document.getElementById(`imdbId${i}`).value, "like", i)
            Rating(i)
            ChangeContent("liked", i)
        })
        document.getElementById(`dislike-btn${i}`).addEventListener("click", async function (event) {
            event.preventDefault()
            await testing(document.getElementById(`imdbId${i}`).value, "dislike", i)
            Rating(i)
            ChangeContent("disliked", i)
        })

    }
}

async function activateEventListeners2() {
    console.log("called activateEventListeners")
    for (let i = numberOfReactButtons; i < numberOfReactButtons + 4; i++) {
        console.log("loop körs för knapparna" + i)
        document.getElementById(`like-btn${i}`).addEventListener("click", async function (event) {
            event.preventDefault()
            console.log("preventdefff")
            await testing(document.getElementById(`imdbId${i}`).value, "like", i)
            Rating(i)
            ChangeContent("liked", i)
        })
        document.getElementById(`dislike-btn${i}`).addEventListener("click", async function (event) {
            event.preventDefault()
            console.log("preventdefff")
            await testing(document.getElementById(`imdbId${i}`).value, "dislike", i)
            Rating(i)
            ChangeContent("disliked", i)
        })

    }
}


function onChange() {
    const chosenList = document.getElementById("SelectedList").value
    const headers = document.getElementsByClassName("randomized-movie-header")
    const topListDivs = document.getElementsByClassName("toggleTopList")
    if (chosenList === "Most voted") {
        headers[0].innerHTML = "Top 4 most voted"
        topListDivs[0].hidden = false
        topListDivs[1].hidden = true
        numberOfReactButtons = document.getElementsByClassName("submitBtn").length / 2
        activateEventListeners()

    }
    else {
        headers[0].innerHTML = "Top 4 best rated"
        topListDivs[0].hidden = true;
        topListDivs[1].hidden = false;
        numberOfReactButtons = document.getElementsByClassName("submitBtn2").length  /2
        activateEventListeners2()
        for (let i = 4; i < numberOfReactButtons + 4; i++) {
            Rating(i)
        }
    }

}

document.getElementById("SelectedList").addEventListener("change", onChange)