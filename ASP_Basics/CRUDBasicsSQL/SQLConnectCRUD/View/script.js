// Wait for the DOM to be fully loaded
document.addEventListener('DOMContentLoaded', function() {
    const staffForm = document.getElementById('staffForm');
    const staffTableBody = document.getElementById('staffTableBody');

    staffForm.addEventListener('submit', async function(e) {
        e.preventDefault();
        
        const staffCount = document.getElementById('staffCount').value;
        
        try {
            // Show loading state
            staffTableBody.innerHTML = '<tr><td colspan="8">Loading...</td></tr>';
            
            // Call your .NET endpoint
            const response = await fetch(`/api/staff/generate?count=${staffCount}`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                }
            });

            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }

            const staffMembers = await response.json();
            
            // Clear the loading message
            staffTableBody.innerHTML = '';
            
            // Populate the table with the response data
            staffMembers.forEach(staff => {
                const row = document.createElement('tr');
                row.innerHTML = `
                    <td>${staff.id}</td>
                    <td>${staff.firstName}</td>
                    <td>${staff.lastName}</td>
                    <td>${staff.gender}</td>
                    <td>${staff.email}</td>
                    <td>${staff.department}</td>
                    <td>$${staff.salary.toLocaleString()}</td>
                    <td>${staff.city}</td>
                `;
                staffTableBody.appendChild(row);
            });

        } catch (error) {
            console.error('Error:', error);
            staffTableBody.innerHTML = `
                <tr>
                    <td colspan="8" class="error-message">
                        Error loading staff members. Please try again.
                    </td>
                </tr>
            `;
        }
    });

    // Optional: Add input validation
    const staffCount = document.getElementById('staffCount');
    staffCount.addEventListener('input', function() {
        const value = parseInt(this.value);
        if (value < 1) this.value = 1;
        if (value > 10) this.value = 10;
    });
});