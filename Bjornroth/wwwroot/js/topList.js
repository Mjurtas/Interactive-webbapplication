let numberOfReactButtons
let countOfNumberOfTimesSwitched = 0

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
    const ratingLabel = document.getElementsByClassName("rating-percentage-label2")
    if (ratingPercentage) {
        ratingLabel[i-4].innerHTML = ratingPercentage.toString() + "%"
    }
    else if (likes || dislikes) {
        ratingLabel[i-4].innerHTML = ratingPercentage.toString() + "%"
    }
    else {
        ratingLabel[i-4].innerHTML = "N/A"
    }

    if (ratingPercentage > 50) {
        ratingLabel[i-4].style.color = "green"
    }
    else if (ratingPercentage < 50) {
        ratingLabel[i-4].style.color = "red"
    }
    else {
        ratingLabel[i-4].style.color = "yellow"
    }
}

function ChangeContent(rating, i) {
    var likebtn = document.getElementById(`like-btn${i}`)
    var dislikebtn = document.getElementById(`dislike-btn${i}`)

    if (rating == "liked") {
        likebtn.parentNode.parentNode.innerHTML = "<h1 style='color: green'>LIKED</h1>"
    }

    else {
        dislikebtn.parentNode.parentNode.innerHTML = "<h1 style='color: red'>DISLIKED</h1>"
    }

}

async function activateEventListeners2() {
    console.log("called activateEventListeners")
    for (let i = 4; i < 8; i++) {
        console.log("loop körs för knapparna" + i)
        document.getElementById(`like-btn${i}`).addEventListener("click", async function (event) {
            event.preventDefault()
            console.log("preventdefff")
            await testing(document.getElementById(`imdbId${i}`).value, "like", i)
            console.log("undra om detta sparas" + i)
            ChangeContent("liked", i)
        })
        document.getElementById(`dislike-btn${i}`).addEventListener("click", async function (event) {
            event.preventDefault()
            console.log("preventdefff")
            await testing(document.getElementById(`imdbId${i}`).value, "dislike", i)
            console.log("undra om detta sparas" + i)
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
    }
    else {
        headers[0].innerHTML = "Top 4 best rated"
        topListDivs[0].hidden = true;
        topListDivs[1].hidden = false;
        numberOfReactButtons = document.getElementsByClassName("submitBtn2").length / 2
        if (countOfNumberOfTimesSwitched === 0) {
            activateEventListeners2()
            countOfNumberOfTimesSwitched++
        for (let i = 4; i < numberOfReactButtons + 4; i++) {
            Rating(i)
            }
        }
    }

}

document.getElementById("SelectedList").addEventListener("change", onChange)