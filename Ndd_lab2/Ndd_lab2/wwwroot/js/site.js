document.addEventListener("DOMContentLoaded", () => {
    const form = document.querySelector("#navbarSearchForm");
    const input = document.querySelector("#globalSearch");

    const filterTables = () => {
        const searchText = input.value.trim().toLocaleLowerCase();
        document.querySelectorAll("table[data-searchable] tbody tr").forEach((row) => {
            row.hidden = !row.textContent.toLocaleLowerCase().includes(searchText);
        });
    };

    input?.addEventListener("input", filterTables);
    form?.addEventListener("submit", (event) => {
        event.preventDefault();
        filterTables();
    });
});
