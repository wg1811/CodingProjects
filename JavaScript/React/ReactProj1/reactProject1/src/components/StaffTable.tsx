import { useState } from "react";
import "../App.css";

let theStatus: boolean = false;
//console.log(theStatus + " before StaffTable.");

const peopleList = ["Joakim", "Sigrid", "Javier", "Antoine"];

const StaffTable = () => {
  const [people, setPeople] = useState<string[]>([]);
  //console.log(theStatus + " before getPeople().");

  const getPeople = () => {
    theStatus = !theStatus;
    // console.log(theStatus);
    if (theStatus) {
      setPeople(peopleList);
    } else setPeople(["Oh snap!"]);
  };
  return (
    <>
      <h3>Staff List</h3>
      <button onClick={getPeople}>
        {people.length > 0 ? "Hide People" : "Show People"}
      </button>

      {people.length > 0 && (
        <table className="staff-table">
          <thead>
            <tr>
              <th>#</th>
              <th>Name</th>
            </tr>
          </thead>
          <tbody>
            {people.map((person, index) => (
              <tr key={index}>
                <td>{index + 1}</td>
                <td>{person}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </>
  );
};

export default StaffTable;
