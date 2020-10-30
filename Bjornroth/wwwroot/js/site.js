async function testing(imdbId, newRating, index) {
    const baseUrl = "https://localhost:44313/api/"
    console.log(imdbId)
    try {
        const api = await fetch(`${baseUrl}movie/${imdbId}/${newRating}`, {
            method: 'GET',
            mode: 'cors'
        })
        if (api.ok) {
            const readableApi = await api.json()
            console.log(readableApi.numberOfLikes)
            if (newRating === 'dislike') {
                document.getElementById(`dislike-number${index}`).innerHTML = readableApi.numberOfDislikes
            }
            else {
                document.getElementById(`like-number${index}`).innerHTML = readableApi.numberOfLikes
            }
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
    const likes = parseInt(document.getElementById(`like-number${i}`).innerText)
    console.log(likes)
    const dislikes = parseInt(document.getElementById(`dislike-number${i}`).innerText)
    console.log(dislikes)
    const ratingPercentage = Math.round(((likes / (likes + dislikes) * 100))).toString()
    console.log(ratingPercentage)
    const ratingLabel = document.getElementsByClassName("rating-percentage-label")
    ratingLabel[0].innerHTML = ratingPercentage + "%"
}

function helperSearch() {
    const search = document.getElementById("searchInput").value.toLowerCase()
    const recommendedResults = document.getElementsByTagName("tr")
   
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
}



let numberOfReactButtons = document.getElementsByClassName("submitBtn").length / 2

function activateEventListeners() {
    for (let i = 0; i < numberOfReactButtons; i++) {
        document.getElementById(`like-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
            testing(document.getElementById(`imdbId${i}`).value, "like", i)
            Rating(i)
        })
        document.getElementById(`dislike-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
            testing(document.getElementById(`imdbId${i}`).value, "dislike", i)
            Rating(i)
        })
    }
    document.getElementById("searchInput").addEventListener("input", helperSearch)

}


activateEventListeners()
Rating(0)