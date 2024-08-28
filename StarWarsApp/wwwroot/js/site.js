// Make text on sort button change between "Sort by Name" and "Sort by Crew"

document.addEventListener("DOMContentLoaded", function() {
    const sortButton = document.getElementById("sortButton");
    let isSortedByName = localStorage.getItem("sortState") === "name";

    function updateButtonText() {
        if (isSortedByName) {
            sortButton.textContent = "Sort by Crew";
        } else {
            sortButton.textContent = "Sort by Name";
        }
    }

    updateButtonText();

    sortButton.addEventListener("click", function(event) {
        event.preventDefault(); 

        isSortedByName = !isSortedByName; 
        updateButtonText(); 

        
        
        localStorage.setItem("sortState", isSortedByName ? "name" : "crew");

        const newSortBy = isSortedByName ? "Name" : "Crew";
        window.location.href = `/Spaceships/Sort?sortBy=${newSortBy}`;

    });
});
