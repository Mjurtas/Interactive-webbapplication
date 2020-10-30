async function testing(imdbId, newRating) {
    const baseUrl = "http://localhost:5000/api/"
    console.log(imdbId)
   
    try {
       const response = await fetch(`${baseUrl}movie/${imdbId}/${newRating}`, {
            method: 'GET'
       })
        console.log(response.status)
        console.log(response.body)
        const parsedResponse = await response.json()
        console.log(parsedResponse)
        
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

function activateEventListeners() {
    for (let i = 0; i < numberOfReactButtons; i++) {
        document.getElementById(`like-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
            testing(document.getElementById(`imdbId${i}`).value, "like")
                UpdateRatingInView(i, "like")
        })
        document.getElementById(`dislike-btn${i}`).addEventListener("click", function (event) {
            event.preventDefault()
            testing(document.getElementById(`imdbId${i}`).value, "dislike")
            UpdateRatingInView(i, "dislike")
        })
    }
    document.getElementById("searchInput").addEventListener("input", helperSearch)

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


activateEventListeners()