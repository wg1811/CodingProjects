//  All components are functions
interface IButton {
  name: string;
}

function Button(props: IButton) {
  return <button className="button">{props.name}</button>;
}

export default Button;
