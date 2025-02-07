from faker import Faker
import random

# Initialize Faker
fake = Faker()

# Define possible job positions
positions = ["Receptionist", "Cleaner", "Operations Manager", "Data Analyst", "Project Manager", "HR Specialist", 
             "Sales Representative", "Marketing Manager", "Customer Support", "IT Technician"]

# Generate and print SQL INSERT statements
print("INSERT INTO staff (name, position, salary, birthdate, hiredate) VALUES")
values = []
for _ in range(500):  # Generate 500 fake employees
    name = fake.name().replace("'", "''")  # Escape single quotes
    position = random.choice(positions)  # Random job position
    salary = round(random.uniform(35000, 120000), 2)  # Salary between 35k and 120k
    birthdate = fake.date_of_birth(minimum_age=22, maximum_age=65)  # Realistic birth date
    hiredate = fake.date_between(start_date="-20y", end_date="today")  # Hired within the last 20 years

    values.append(f"('{name}', '{position}', {salary}, '{birthdate}', '{hiredate}')")

# Print as a valid SQL INSERT statement
print(",\n".join(values) + ";")