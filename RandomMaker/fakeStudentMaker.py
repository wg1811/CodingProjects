from faker import Faker
import random

# Initialize Faker
fake = Faker()

# How many?
count = 100

def generate_fake_people(count):
    """Generate a list of fake people with various attributes."""
    people = []
    
    for _ in range(count):
        person = {
            "name": fake.name(),
            "email": fake.email(),
            "age": random.randint(18, 80),
        }
        people.append(person)
    
    return people

# Generate 10 fake people
fake_people = generate_fake_people(count)

# Print the results
for person in fake_people:
    values = f"('{person['name']}', '{person['email']}', {person['age']}),"
    print(values)