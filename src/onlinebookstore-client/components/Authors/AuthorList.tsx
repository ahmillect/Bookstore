import { Card, CardContent, Grid, Typography } from "@mui/material";
import { useQuery } from "urql";

type Author = {
  id: string;
  name: string;
  bio: string;
};

const AUTHORS_QUERY = `
query {
    authors {
        id
        name
        bio
        books {
            title
        }
    }
}
`;

function AuthorList() {
  const [result] = useQuery({ query: AUTHORS_QUERY });
  const { data, fetching, error } = result;

  if (fetching) return <p>Loading...</p>;
  if (error) return <p>Error: {error.message}</p>;

  return (
    <Grid container spacing={3}>
      {data.authors.map((author: Author) => (
        <Grid item xs={12} sm={6} md={4} key={author.id}>
          <Card>
            <CardContent>
              <Typography variant="h5" component="div">
                {author.name}
              </Typography>
              <Typography variant="body2" color="textSecondary">
                {author.bio}
              </Typography>
            </CardContent>
          </Card>
        </Grid>
      ))}
    </Grid>
  );
}

export default AuthorList;
