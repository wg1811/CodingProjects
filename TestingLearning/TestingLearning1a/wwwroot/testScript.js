document.getElementById("questions-form").addEventListener("submit", (e) => {
    e.preventDefault();

    //get the form
    const form = e.target;
    //get the answers
    const inputs = form.querySelectorAll("input[type='radio']:checked");
    form.reset();

    const userAnswers = {};

    inputs.forEach(input => {
        userAnswers[input.name] = input.value;
    });

    fetch("http://localhost:5277/test", {
        method: "POST",
        headers: {
            "Content-Type" : "application/json"
        },
        body: JSON.stringify(userAnswers)
    })
    .then ((response) => {
        if(!response.ok) {
            throw new Error("Failed to fetch");
        }
        return response.json();
    })
    .then((data) => {
        console.log(data);
        Swal.fire({
            title:"Test Score:",
            text:`You ${data.situation}.`,
            icon: "success"
        })
    })
    .catch((error) => {
        console.error("error", error);
    })
});



