const apiUrl = "https://localhost:59102/api/Cars";

const loadCarsBtn = document.getElementById("loadCarsBtn");
const carsContainer = document.getElementById("cars");
const searchInput = document.getElementById("searchInput");
const carsCount = document.getElementById("carsCount");

let loadedCars = [];

loadCarsBtn.addEventListener("click", loadCars);

async function loadCars() {
    try {
        carsContainer.innerHTML = `<p class="loading">Loading cars...</p>`;

        const response = await fetch(apiUrl);

        if (!response.ok) {
            throw new Error("Failed to load cars");
        }

        const cars = await response.json();

        loadedCars = cars;

        renderCars(loadedCars);
    } catch (error) {
        carsContainer.innerHTML =
            `<p class="error">Failed to load cars from the API.</p>`;

        console.error(error);
    }
}

function renderCars(cars) {
    carsCount.textContent = `Available cars: ${cars.length}`;

    carsContainer.innerHTML = "";

    if (cars.length === 0) {
        carsContainer.innerHTML = `
            <div class="empty-state">
                No cars found.
            </div>
        `;

        return;
    }

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

                <div class="category">
                    ${car.categoryName}
                </div>
            </div>
        `;

        carsContainer.appendChild(card);
    });
}

searchInput.addEventListener("input", function () {
    const searchTerm = this.value.toLowerCase();

    const filteredCars = loadedCars.filter(car =>
        car.brand.toLowerCase().includes(searchTerm) ||
        car.model.toLowerCase().includes(searchTerm)
    );

    renderCars(filteredCars);
});

document.getElementById("sortSelect")
    .addEventListener("change", function () {

        const sortedCars = [...loadedCars];

        if (this.value === "low") {
            sortedCars.sort((a, b) => a.pricePerDay - b.pricePerDay);
        }

        if (this.value === "high") {
            sortedCars.sort((a, b) => b.pricePerDay - a.pricePerDay);
        }

        renderCars(sortedCars);
    });
      