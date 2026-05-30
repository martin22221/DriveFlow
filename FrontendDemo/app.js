const apiUrl = "https://localhost:59102/api/Cars";

const loadCarsBtn = document.getElementById("loadCarsBtn");
const carsContainer = document.getElementById("cars");

loadCarsBtn.addEventListener("click", loadCars);

async function loadCars() {
    try {
        carsContainer.innerHTML = `<p class="loading">Loading cars...</p>`;

        const response = await fetch(apiUrl);

        if (!response.ok) {
            throw new Error("Failed to load cars");
        }

        const cars = await response.json();

        carsContainer.innerHTML = "";

        cars.forEach(car => {
            const card = document.createElement("div");
            card.className = "card";

            card.innerHTML = `
                <img src="${car.imageUrl}" alt="${car.brand} ${car.model}">

                <div class="card-content">
                    <h2>${car.brand} ${car.model}</h2>
                    <p><strong>Year:</strong> ${car.year}</p>
                    <p class="price">${car.pricePerDay} lv/day</p>
                    <p>${car.description ?? "No description available."}</p>
                    <div class="category">${car.categoryName}</div>
                </div>
            `;

            carsContainer.appendChild(card);
        });

    } catch (error) {
        carsContainer.innerHTML = `<p class="error">Failed to load cars from the API.</p>`;
        console.error(error);
    }
}