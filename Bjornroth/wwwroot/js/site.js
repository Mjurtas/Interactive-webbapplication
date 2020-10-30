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

function Rating() {
    const likes = parseInt(document.getElementByClassName("number-of-likes-label"))
    const dislikes = parseInt(document.getElementsByClassName("number-of-dislikes-label"))
    const ratingPercentage = Math.round(((likes / (likes + dislikes) * 100)))
    document.getElementsByClassName("rating-percentage-label").innerHTML = ratingPercentage + "%"
}

function helperSearch() {
    const search = document.getElementById("searchInput").value
    const recommendedResults = document.getElementsByTagName("tr")
   
    let count = 0
    for (var i = 1; i < recommendedResults.length; i++) {

        if (recommendedResults[i].cells[1].innerText.includes(search) && search != "" && count < 5) {
           
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
        })
        document.getElementById(`dislike-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
            testing(document.getElementById(`imdbId${i}`).value, "dislike", i)
        })
    }
    document.getElementById("searchInput").addEventListener("input", helperSearch)

}


activateEventListeners()
Rating()