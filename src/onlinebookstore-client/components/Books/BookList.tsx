import { useQuery } from "urql";

interface Book {
  id: string;
  title: string;
  description: string;
}

const BOOKS_QUERY = `
query {
    books(where: { or: [{ title: { contains: "C#" } }] }) {
        id
        title
        price
        author {
            name
        }
    }
}
`;

function BookList() {
  const [result] = useQuery({ query: BOOKS_QUERY });
  const { data, fetching, error } = result;

  if (fetching) return <p>Loading...</p>;
  if (error) return <p>Error: {error.message}</p>;

  return (
    <div>
      {data.books.map((book: Book) => (
        <div key={book.id}>
          <h2>{book.title}</h2>
          <p>{book.description}</p>
        </div>
      ))}
    </div>
  );
}

export default BookList;
