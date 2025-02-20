//  All components are functions
interface Iheading {
  content: string;
}

function Heading(props: Iheading) {
  return <h1 className="heading">{props.content}</h1>;
}

export default Heading;
