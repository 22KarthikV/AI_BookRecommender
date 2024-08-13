import csv
import random
from tqdm import tqdm

def generate_csv_files(num_books, num_users, approx_num_ratings):
    # Generate books
    books = []
    isbns = set()
    for i in tqdm(range(num_books), desc="Generating books"):
        while True:
            isbn = f'{random.randint(1000000000, 9999999999)}'
            if isbn not in isbns:
                isbns.add(isbn)
                break
        book_title = f'Book Title {i+1}'
        author = f'Author {random.randint(1, num_books//10)}'  # Reuse authors for realism
        year = random.randint(1950, 2023)
        publisher = f'Publisher {random.randint(1, num_books//50)}'  # Reuse publishers for realism
        image_url = f'http://images.amazon.com/images/P/{isbn}.01'
        books.append([isbn, book_title, author, year, publisher, f'{image_url}.THUMBZZZ.jpg', f'{image_url}.MZZZZZZZ.jpg', f'{image_url}.LZZZZZZZ.jpg'])

    # Generate users
    users = []
    user_ids = list(range(1, num_users + 1))
    for user_id in tqdm(user_ids, desc="Generating users"):
        age = random.randint(18, 80)
        city = f'City {random.randint(1, num_users//10)}'
        state = f'State {random.randint(1, 50)}'
        country = 'USA'
        users.append([user_id, age, city, state, country])

    # Generate ratings
    ratings = []
    rated_books = set()
    target_ratings_per_user = approx_num_ratings // num_users

    for user_id in tqdm(user_ids, desc="Generating ratings"):
        user_ratings = random.randint(1, min(len(isbns), target_ratings_per_user * 2))
        user_rated_books = random.sample(list(isbns), user_ratings)
        for isbn in user_rated_books:
            rating = random.randint(1, 10)
            ratings.append([user_id, isbn, rating])
            rated_books.add(isbn)

    # Ensure all books have at least one rating
    unrated_books = isbns - rated_books
    for isbn in tqdm(unrated_books, desc="Rating unrated books"):
        user_id = random.choice(user_ids)
        rating = random.randint(1, 10)
        ratings.append([user_id, isbn, rating])

    # Write to CSV files
    with open('books.csv', 'w', newline='', encoding='utf-8') as f:
        writer = csv.writer(f)
        writer.writerow(['ISBN', 'BookTitle', 'BookAuthor', 'YearOfPublication', 'Publisher', 'ImageUrlSmall', 'ImageUrlMedium', 'ImageUrlLarge'])
        writer.writerows(books)

    with open('users.csv', 'w', newline='', encoding='utf-8') as f:
        writer = csv.writer(f)
        writer.writerow(['UserID', 'Age', 'City', 'State', 'Country'])
        writer.writerows(users)

    with open('ratings.csv', 'w', newline='', encoding='utf-8') as f:
        writer = csv.writer(f)
        writer.writerow(['UserID', 'ISBN', 'Rating'])
        writer.writerows(ratings)

    print(f"\nCSV files generated successfully!")
    print(f"Total books: {len(books)}")
    print(f"Total users: {len(users)}")
    print(f"Total ratings: {len(ratings)}")
    print(f"Books with ratings: {len(rated_books)}")

# Set your desired dataset sizes here
num_books = 10000
num_users = 2000
approx_num_ratings = 100000

generate_csv_files(num_books, num_users, approx_num_ratings)