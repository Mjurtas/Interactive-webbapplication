const percentageBubble = document.getElementsByClassName("percentageBubble")
console.log(percentageBubble.length)

function updatePercentages(id) {
    const numberOfLikes = parseInt(document.getElementById(`like-number${id}`).innerHTML)
    const numberOfDislikes = parseInt(document.getElementById(`dislike-number${id}`).innerHTML)
    const theWhole = numberOfLikes + numberOfDislikes
    const percentage = Math.round(((numberOfLikes / theWhole) * 100))
    if (percentage > 50) {
        percentageBubble[id].style.backgroundColor = "green"
    }
    else if (percentage < 50) {
        percentageBubble[id].style.backgroundColor = "red"
    }
    else {
        percentageBubble[id].style.backgroundColor = "yellow"
    }
    percentageBubble[id].children[0].innerHTML = percentage.toString() + "%"
}

//function test() {
//    for (let i = 0; 1 < percentageBubble; i++) {
//        updatePercentages(i)
//        console.log(i)
//    }
//}

//test()
updatePercentages(0)
updatePercentages(1)
updatePercentages(2)
updatePercentages(3)
