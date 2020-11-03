﻿async function testing(imdbId, newRating, index) {
    const baseUrl = "https://localhost:5001/api/"
    //const baseUrl = "https://localhost:44313/api/"
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
/* This function is fired by button click (assigned in the AddEventListeners-function. 
   i-value is passed in, and generetes the rating-score for each rating section in each film. */
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

/*1. Value is submitted by the user in the text-input.
 * 2. A "searchTableRow is generetaed in the HTML for every movie in CMDb database, and is by default "hidden".
 * 3. The if-case compares the searchinput to every movietitle in the table (CMDb), if it's true, that row is visible.
 * To limit the search to 5, a counter is initially set to 0 and increments every time the if-case = true. */

function helperSearch() {
    const search = document.getElementById("searchInput").value.toLowerCase()
    const recommendedResults = document.getElementsByClassName("searchTableRow")
   
    let count = 0
    for (var i = 1; i < recommendedResults.length; i++) {
        const movieTitle = recommendedResults[i].cells[1].innerText.toLowerCase();
        if (movieTitle.includes(search) && search != "" && count < 5) {

            recommendedResults[i].hidden = false;
            recommendedResults[i].cells[0].hidden = false;
            recommendedResults[i].cells[1].hidden = false;
            recommendedResults[i].cells[2].hidden = false;

            count += 1
        }
        else {

            recommendedResults[i].hidden = true;
            recommendedResults[i].cells[0].hidden = true;
            recommendedResults[i].cells[1].hidden = true;
            recommendedResults[i].cells[2].hidden = true;


        }
    }

    let searchValue = document.getElementById("search-input-holder")
    searchValue.value = search;
    document.getElementById("search-value-btn").value = searchValue.value
    if (search != "") {
        document.getElementById("suggestion-search").hidden = false;
    }

    else {
        document.getElementById("suggestion-search").hidden = true;
    }
    updateSearchRatings()
}

function updateSearchRatings() {
    const row = document.getElementsByClassName("searchTableRow")
    const likeColumns = document.getElementsByClassName("numberOfLikes-td")
    const dislikeColumns = document.getElementsByClassName("numberOfDislikes-td")
    for (var i = 0; i < row.length; i++) {
        let likes = parseInt(likeColumns[i].innerText)
        let dislikes = parseInt(dislikeColumns[i].innerText)
        let rating = Math.round(((likes / (likes + dislikes) * 100)))
        row[i].children[2].lastChild.innerText = rating.toString() + "%"

        if (rating > 50) {
            row[i].children[2].lastChild.style.color = "green"
        }
        else if (rating < 50) {
            row[i].children[2].lastChild.style.color = "red"
        }
        else {
            row[i].children[2].lastChild.style.color = "yellow"
        }
    }
}

function movieDetails() {
    const nmbrOfLikesLabel = document.getElementById("nmbr-of-likes")
    const nmbrOfDislikesLabel = document.getElementById("nmbr-of-dislikes")

}


/*There are 2 forms per rating section, with 2 submitBtn each. Therefore, the length of class name is divided by 2, and then like/dislike
 btn is assigned to a eventlistener in the same indexed loopround.
 
 Then, this function adds eventlisteners on every element with the corresponding id. Since the elements in the HTML are generated with
 a loop where "i" is inserted at the end of the Id, every buttons is assigned to an eventlisteners. To assign the rating percentage to its label, the
 Rating(i)-function is fired at the end of every loop, generating visual presentation of rating to each element.*/

let numberOfReactButtons = document.getElementsByClassName("submitBtn").length / 2

async function activateEventListeners() {
    for (let i = 0; i < numberOfReactButtons; i++) {
        document.getElementById(`like-btn${i}`).addEventListener("click", async function (event) {
            event.preventDefault()
            await testing(document.getElementById(`imdbId${i}`).value, "like", i)
            Rating(i)
        })
        document.getElementById(`dislike-btn${i}`).addEventListener("click", async function (event) {
            event.preventDefault()
            await testing(document.getElementById(`imdbId${i}`).value, "dislike", i)
            Rating(i)
        })

    }
    document.getElementById("searchInput").addEventListener("input", helperSearch)
}

function setRatingLabels() {
    for (let i = 0; i < numberOfReactButtons; i++) {
        Rating(i)
    }
}


activateEventListeners()
setRatingLabels()