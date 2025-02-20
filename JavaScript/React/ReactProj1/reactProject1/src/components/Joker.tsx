import { useState, useEffect } from "react";

// Define the type for our joke
interface Joke {
  setup: string;
  punchline: string;
}

const Joker = () => {
  // Initialize with proper typing
  const [theJoke, setJoke] = useState<Joke | null>(null);
  const [showPunchline, setShowPunchline] = useState(false);

  // Reset showPunchline when a new joke is fetched
  useEffect(() => {
    if (theJoke) {
      setShowPunchline(false);
      const timer = setTimeout(() => {
        setShowPunchline(true);
      }, 2000);

      // Cleanup the timer if component unmounts or new joke is fetched
      return () => clearTimeout(timer);
    }
  }, [theJoke]);

  const tellJoke = async () => {
    try {
      const response = await fetch(
        "https://official-joke-api.appspot.com/random_joke"
      );
      const data = await response.json();
      setJoke(data);
    } catch (error) {
      console.error("Error fetching joke:", error);
      setJoke(null);
    }
  };

  return (
    <>
      <h3>Random Joke Generator</h3>
      <button onClick={tellJoke}>Get Joke</button>
      {theJoke && (
        <>
          <p>{theJoke.setup}</p>
          {showPunchline && <p>{theJoke.punchline}</p>}
        </>
      )}
    </>
  );
};

export default Joker;
