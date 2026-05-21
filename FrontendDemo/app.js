const apiUrl = "https://localhost:5001/api/cars";

document.getElementById("loadCarsBtn").addEventListener("click", async () => {
    const response = await fetch(apiUrl);
    const cars = await response.json();

    const container = document.getElementById("cars");
    container.innerHTML = "";

    cars.forEach(car => {
        const card = document.createElement("div");
        card.className = "card";
        card.innerHTML = `
            <h2>${car.brand} ${car.model}</h2>
            <p>Year: ${car.year}</p>
            <p>Price per day: ${car.pricePerDay} лв.</p>
            <p>Category: ${car.categoryName}</p>
        `;
        container.appendChild(card);
    });
});
