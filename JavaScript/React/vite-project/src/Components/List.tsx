//  All components are functions
// interface IList {
//   items: string[];
// }

// function List({items}: IList) {
//   return (
//     <ul>
//       {items.map((item, index) => (
//         <li key={index}>{item}</li>
//       ))}
//     </ul>
//   );

// export default List;

// Define the shape of each list item
interface ListItem {
  id: string | number;
  content: string;
  // Optional additional fields
  url?: string;
  isActive?: boolean;
}

type ListProps = {
  items: ListItem[];
  // Optional props for customization
  variant?: "ordered" | "unordered" | "none";
  onItemClick?: (item: ListItem) => void;
  className?: string;
  emptyMessage?: string;
};

const List = ({
  items,
  variant = "unordered",
  onItemClick,
  className = "",
  emptyMessage = "No items to display",
}: ListProps) => {
  // Early return for empty list
  if (items.length === 0) {
    return <p className="text-gray-500 italic">{emptyMessage}</p>;
  }

  // Handle item click with proper type safety
  const handleClick = (item: ListItem) => {
    if (onItemClick) {
      console.log(`You clicked it! The ${item}`);
    }
  };

  // Determine list style
  const listStyle =
    variant === "none"
      ? "list-none"
      : variant === "ordered"
      ? "list-decimal"
      : "list-disc";

  // Render appropriate list type
  const ListElement = variant === "ordered" ? "ol" : "ul";

  return (
    <ListElement className={`pl-6 ${listStyle} ${className}`}>
      {items.map((item) => (
        <li
          key={item.id}
          className={`py-1 ${item.isActive ? "font-semibold" : ""} ${
            onItemClick ? "cursor-pointer hover:text-blue-600" : ""
          }`}
          onClick={() => handleClick(item)}
        >
          {item.url ? (
            <a href={item.url} className="hover:underline">
              {item.content}
            </a>
          ) : (
            item.content
          )}
        </li>
      ))}
    </ListElement>
  );
};

export default List;
