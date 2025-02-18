from faker import Faker
import random

# Initialize Faker
fake = Faker()

def generate_fake_people(count=10):
    """Generate a list of fake people with various attributes."""
    people = []
    
    for _ in range(count):
        person = {
            "name": fake.name(),
            "email": fake.email(),
            "age": random.randint(18, 80),
            "job": fake.job(),
            "address": fake.address(),
            "phone": fake.phone_number(),
        }
        people.append(person)
    
    return people

# Generate 10 fake people
fake_people = generate_fake_people(10)

# Print the results
for person in fake_people:
    print(person)
