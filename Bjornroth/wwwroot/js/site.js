async function testing(imdbId, newRating, index) {
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

// Function that filters through the movies in cdmb's database according to the search. Showcases the five most relevant
// movies according to three filters. Firstly it filters through to show the movies that title begins with the searched string, 
// secondly shows movies whoose title has a word starting with the search term 
// and lastly shows movies where the searched term is included somwhere in the title.

function helperSearch() {

    // Define the search term to filter the movies with
    const search = document.getElementById("searchInput").value.toLowerCase()

    // Get all of the movies to filter through
    const recommendedResults = document.getElementsByClassName("searchTableRow")

    // Define the arrays that the next 2 loops will filter through
    let recommendedResultsToFilterFurther = []
    let recommendedResultsToFilterALastTime = []

    // The count of how many movies are beign showcased
    let count = 0

    // Reorders the table to its original order
    resetTableOrder(recommendedResults)

    // Sets the sixth's rows value to the searched term to let the user easily navigate to further search
    let searchValue = document.getElementById("search-input-holder")
    searchValue.value = search;
    document.getElementById("search-value-btn").value = searchValue.value

    // Filter the movies if the search has a value
    if (search != "") {

        // Display the sixth row
        document.getElementById("suggestion-search").hidden = false;

        // Loops through all the movies
        for (let i = 0; i < recommendedResults.length; i++) {

            const movieTitle = recommendedResults[i].cells[1].innerText.toLowerCase();
            // Shows the row displaying the movie if the title starts with the searchterm
            if (movieTitle.startsWith(search) && count < 5) {

                recommendedResults[i].hidden = false;
                recommendedResults[i].cells[0].hidden = false;
                recommendedResults[i].cells[1].hidden = false;
                recommendedResults[i].cells[2].hidden = false;

                // Rearranges the movielist to ensure the movie starting with the searchterm is displayed at the top
                recommendedResults[i].parentNode.insertBefore(recommendedResults[i], recommendedResults[count]);
                count += 1

            }

            // If the movietitle doesn't include the searchterm, hide it
            else {

                recommendedResults[i].hidden = true;
                recommendedResults[i].cells[0].hidden = true;
                recommendedResults[i].cells[1].hidden = true;
                recommendedResults[i].cells[2].hidden = true;

                // But if the Movie title includes the searched term, then it is added to an array to iterate through later
                // if there are less than 5 movies showing after this loop 
                if (movieTitle.includes(search)) {
                    recommendedResultsToFilterFurther.push(recommendedResults[i])
                }
            }
        }
        if (count < 5) {
            for (let i = 0; i < recommendedResultsToFilterFurther.length; i++) {
                const movieTitle = recommendedResultsToFilterFurther[i].cells[1].innerText.toLowerCase();

                // Splits the movie's title into substrings so every word can be compared to the searched term
                const movieTitleSubStrings = movieTitle.split(" ")

                // If a movie has a word that starts with the searched term, then it will be showcased
                for (let j = 0; j < movieTitleSubStrings.length; j++) {
                    if (movieTitleSubStrings[j].startsWith(search) && count < 5) {

                        recommendedResultsToFilterFurther[i].hidden = false;
                        recommendedResultsToFilterFurther[i].cells[0].hidden = false;
                        recommendedResultsToFilterFurther[i].cells[1].hidden = false;
                        recommendedResultsToFilterFurther[i].cells[2].hidden = false;

                        // Rearranges the movielist to ensure the movie is displayed underneath the better 
                        // matching results but over the results that doesn't match as well
                        recommendedResults[i].parentNode.insertBefore(recommendedResultsToFilterFurther[i], recommendedResults[count]);
                        count += 1

                        // Stops the iteration of substrings if one substring already matched the searhed term
                        j = movieTitleSubStrings.length

                        // If a movie has been added to the final array to filter through but one of the words in the movie's 
                        // title begins with the searched term, it is removed from the array because it is already being displayed
                        if (recommendedResultsToFilterALastTime.includes(recommendedResultsToFilterFurther[i]))
                        {
                            recommendedResultsToFilterALastTime.pop()
                        }
                    }

                    // If the movies first word doesn't start with the searched term but the movie title includes it,
                    // it is added to the final array to filter through if there still aren't 5 movies that are being displayed
                    else if (movieTitle.includes(search) && j === 0) {
                        recommendedResultsToFilterALastTime.push(recommendedResultsToFilterFurther[i])
                    }
                }
            }
        }

        // If there still aren't 5 movies beign showcased then this will showcase movies where the searched term is included in the title
        if (count < 5) {
            for (let i = 0; i < recommendedResultsToFilterALastTime.length; i++) {
                const movieTitle = recommendedResultsToFilterALastTime[i].cells[1].innerText.toLowerCase();
                if (movieTitle.includes(search) && count < 5) {
                    recommendedResultsToFilterALastTime[i].hidden = false;
                    recommendedResultsToFilterALastTime[i].cells[0].hidden = false;
                    recommendedResultsToFilterALastTime[i].cells[1].hidden = false;
                    recommendedResultsToFilterALastTime[i].cells[2].hidden = false;
                    count += 1
                }

                // If there are 5 movies being displayed, then the job is done!
                else if (count === 5) {
                    break
                }
            }
        }
    }

    // If search has no value, hide all the movies
    else {
        for (let i = 0; i < recommendedResults.length; i++) {
            recommendedResults[i].hidden = true;
            recommendedResults[i].cells[0].hidden = true;
            recommendedResults[i].cells[1].hidden = true;
            recommendedResults[i].cells[2].hidden = true;

            //Hide the sixth row too
            document.getElementById("suggestion-search").hidden = true;
        }
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




function resetTableOrder(rowsToOrder) {

    for (let i = 0; i < rowsToOrder.length; i++) {

        const rowToMove = document.getElementById(`${i}`)
        rowsToOrder[i].parentNode.insertBefore(rowToMove, rowsToOrder[i]);

    }
}


/*There are 2 forms per rating section, with 2 submitBtn each. Therefore, the length of class name is divided by 2, and then like/dislike
 btn is assigned to a eventlistener in the same indexed loopround.*/
let numberOfReactButtons = document.getElementsByClassName("submitBtn").length / 2

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
    document.getElementById("searchInput").addEventListener("input", helperSearch)
}

function setRatingLabels() {
    for (let i = 0; i < numberOfReactButtons; i++) {
        Rating(i)
    }
}


activateEventListeners()
setRatingLabels()