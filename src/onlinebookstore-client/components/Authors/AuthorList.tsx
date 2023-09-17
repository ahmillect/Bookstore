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
    <div>
      {data.authors.map((author: Author) => (
        <div key={author.id}>
          <h2>{author.name}</h2>
          <p>{author.bio}</p>
        </div>
      ))}
    </div>
  );
}

export default AuthorList;
