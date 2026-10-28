(() => {
    const search = document.querySelector("[data-student-search]");
    const table = document.querySelector("[data-student-table]");
    if (!search || !table) return;

    const rows = Array.from(table.querySelectorAll("tbody tr:not([data-empty-state])"));
    const emptyState = table.querySelector("[data-empty-state]");
    const resultCount = document.querySelector("[data-result-count]");

    search.addEventListener("input", () => {
        const query = search.value.trim().toLocaleLowerCase("vi");
        let visibleRows = 0;

        for (const row of rows) {
            const matches = row.textContent.toLocaleLowerCase("vi").includes(query);
            row.hidden = !matches;
            if (matches) visibleRows += 1;
        }

        if (emptyState) emptyState.hidden = visibleRows > 0;
        if (resultCount) resultCount.textContent = `${visibleRows} sinh viên`;
    });
})();
