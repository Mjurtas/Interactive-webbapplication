async function testing(imdbId, newRating, index) {
    const baseUrl = "https://localhost:5001/api/"
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


function Rating() {
    const likes = parseInt(document.getElementById("like-number0").innerHTML)
    console.log(likes)
    const dislikes = parseInt(document.getElementById("dislike-number0").innerHTML)
    console.log(dislikes)
    const ratingPercentage = Math.round(((likes / (likes + dislikes) * 100)))
    console.log(ratingPercentage)
    if (likes + dislikes > 0) {
        document.getElementById("rating-percentage-label").innerHTML = ratingPercentage.toString() + "%"
    }
    else {
        document.getElementById("rating-percentage-label").innerHTML = "N/A"
    }

    if (ratingPercentage > 50) {
        document.getElementById("rating-percentage-label").style.color = "green"
    }
    else if (ratingPercentage < 50) {
        document.getElementById("rating-percentage-label").style.color = "red"
    }

    else { document.getElementById("rating-percentage-label").style.color = "yellow" }
}



function activateEventListeners() {
    for (let i = 0; i < numberOfReactButtons; i++) {
        document.getElementById(`like-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
            Rating()
            testing(document.getElementById(`imdbId${i}`).value, "like", i)
        })
        document.getElementById(`dislike-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
            Rating()
            testing(document.getElementById(`imdbId${i}`).value, "dislike", i)
        })
    }
    document.getElementById("searchInput").addEventListener("input", helperSearch)
}

function ShortenPlot() {
    const fullPlot = document.getElementById("full-plot").innerHTML
    
    const shortenedPlot = fullPlot.substr(0, 140) + "\u2026";
    document.getElementById("shortened-plot").innerHTML = shortenedPlot
}


activateEventListeners()
ShortenPlot()

