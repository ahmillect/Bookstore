import { Card, CardContent, Grid, Typography } from "@mui/material";
import { useQuery } from "urql";

type Book = {
  id: string;
  title: string;
  description: string;
};

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
    <Grid container spacing={3}>
      {data.books.map((book: Book) => (
        <Grid item xs={12} sm={6} md={4} key={book.id}>
          <Card>
            <CardContent>
              <Typography variant="h5" component="div">
                {book.title}
              </Typography>
              <Typography variant="body2" color="textSecondary">
                {book.description}
              </Typography>
            </CardContent>
          </Card>
        </Grid>
      ))}
    </Grid>
  );
}

export default BookList;
