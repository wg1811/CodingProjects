import { Link } from "react-router-dom";

const NavBar = () => {
  return (
    <div id="navbar-group">
      <ul id="navbar">
        <li>
          <Link to="/">Home</Link>
        </li>
        <li>
          <Link to="/students">Students</Link>
        </li>
        <li>
          <Link to="/courses">Courses</Link>
        </li>
      </ul>
    </div>
  );
};

export default NavBar;
