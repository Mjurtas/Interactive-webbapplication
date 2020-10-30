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

    else { document.getElementById("rating-percentage-label").style.color = "yellow"}
}


Rating()