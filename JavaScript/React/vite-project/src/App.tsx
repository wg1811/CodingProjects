import "./App.css";
import Button from "./Components/Button";
import Heading from "./Components/h1";
import List from "./Components/List"; // Add this import

function App() {
  // Define your list items
  const listItems = [
    { id: 1, content: "Learn React" },
    { id: 2, content: "Master TypeScript" },
    { id: 3, content: "Build Projects" },
  ];

  // Optional: Create a click handler
  // const handleListItemClick = (item: { id: number; content: string }) => {
  //   console.log(`You clicked: ${item.content}`);
  // };

  return (
    <>
      <h1>Playing With React</h1>
      <Button name="Click Me" />
      <Button name="Don't Click Me" />
      <Heading content="Hey there." />

      {/* Add the List component */}
      <List
        items={listItems}
        // onItemClick={handleListItemClick}
      />
    </>
  );
}

export default App;
