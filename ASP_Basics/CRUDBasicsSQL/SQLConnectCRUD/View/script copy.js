function getAll() {
    const url = "http://localhost:5128/getstaff";
    fetch(url, {
        method: "GET"
    })
    .then((response) => {
        if (!response.ok) {
            throw new Error('error fetching data');
        }
        return
    })
    .then((data) => {
        console.log(data);
    })
    .catch (error => {
        console.error('error', error)
    });
}

function createStaffMember(newMember) {
const url = "http://localhost:5128/addstaff";
fetch(url, {
    method:"POST",
    headers: {
        "Content-type": "application/JSON"
    },
    body: JSON.stringify(newMember)
})
.then((response) => {
    if (!response.ok) {
        throw new Error ("Failed to fetch response.")
    }
    return response.json();
})
.then((data) => {
    console.log(data);
})
.catch((error) => {
    console.error("error",error);
})
}

function getStaffMemberFromApi() {
const urlApi = "https://randomuser.me/api/?results=10";
fetch(urlApi, {
    method:"GET"
})
.then((response) => {
    if (!response.ok) {
        throw new Error ("Failed to fetch data.")
    }
    return response.json();
})
.then((data) => {
    console.log(data.response + " is the response.");
})
.catch((error) => {
    console.error("error",error);
})
}

getAll();