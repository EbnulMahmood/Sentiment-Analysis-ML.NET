// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const getSentiment = (userInput) => {
    return fetch(`Index?handler=AnalyzeSentiment&text=${userInput}`)
        .then((response) => {
            return response.text();
        })
}

const updateMarker = (markerPosition, sentiment) => {
    $("#markerPosition").attr("style", `left:${markerPosition}%`);
    $("#markerValue").text(sentiment);
}

const updateSentiment = () => {

    const userInput = $("#Message").val();

    getSentiment(userInput)
        .then((sentiment) => {
            switch (sentiment) {
                case "Positive":
                    updateMarker(100.0, sentiment);
                    break;
                case "Negative":
                    updateMarker(0.0, sentiment);
                    break;
                default:
                    updateMarker(45.0, "Neutral");
            }
        });
}

$("#btnSubmit").on('click', updateSentiment)