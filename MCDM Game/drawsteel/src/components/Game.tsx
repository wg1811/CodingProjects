'use client'

import React, { useState } from 'react';

// Basic types we'll need
type Position = {
  x: number;
  y: number;
};

type Unit = {
  id: string;
  position: Position;
  type: 'warrior'; // We'll add more types later
  hasMoved: boolean;
};

// Main Game component
const Game = () => {
  // Game state
  const [units, setUnits] = useState<Unit[]>([
    { id: '1', position: { x: 2, y: 2 }, type: 'warrior', hasMoved: false },
    { id: '2', position: { x: 5, y: 5 }, type: 'warrior', hasMoved: false },
  ]);
  
  const [selectedUnitId, setSelectedUnitId] = useState<string | null>(null);
  const [turn, setTurn] = useState(1);
  
  // Grid configuration
  const GRID_SIZE = 8;
  
  // Handle clicking on a grid cell
  const handleCellClick = (x: number, y: number) => {
    // Find if there's a unit at the clicked position
    const unitAtPosition = units.find(unit => 
      unit.position.x === x && unit.position.y === y
    );
    
    // If we have a selected unit and clicked an empty cell, try to move
    if (selectedUnitId && !unitAtPosition) {
      const selectedUnit = units.find(unit => unit.id === selectedUnitId);
      if (selectedUnit && !selectedUnit.hasMoved) {
        // Move the unit
        setUnits(units.map(unit => 
          unit.id === selectedUnitId
            ? { ...unit, position: { x, y }, hasMoved: true }
            : unit
        ));
        setSelectedUnitId(null);
      }
    }
    // If we clicked a unit, select it
    else if (unitAtPosition && !unitAtPosition.hasMoved) {
      setSelectedUnitId(unitAtPosition.id);
    }
  };
  
  // End the current turn
  const endTurn = () => {
    setTurn(turn + 1);
    setUnits(units.map(unit => ({ ...unit, hasMoved: false })));
    setSelectedUnitId(null);
  };
  
  // Generate the grid
  const renderGrid = () => {
    const grid = [];
    
    for (let y = 0; y < GRID_SIZE; y++) {
      for (let x = 0; x < GRID_SIZE; x++) {
        const unit = units.find(u => u.position.x === x && u.position.y === y);
        const isSelected = unit?.id === selectedUnitId;
        
        grid.push(
          <div
            key={`${x}-${y}`}
            onClick={() => handleCellClick(x, y)}
            className={`
              w-16 h-16 border border-gray-300
              flex items-center justify-center
              ${unit ? 'bg-blue-200' : 'bg-white'}
              ${isSelected ? 'bg-blue-400' : ''}
              ${unit?.hasMoved ? 'opacity-50' : ''}
              hover:bg-gray-100 cursor-pointer
            `}
          >
            {unit && (
              <div className="text-sm font-bold">
                {unit.type}
              </div>
            )}
          </div>
        );
      }
    }
    
    return grid;
  };
  
  return (
    <div className="p-8">
      <div className="mb-4">
        <h1 className="text-2xl font-bold mb-2">Turn: {turn}</h1>
        <button
          onClick={endTurn}
          className="bg-blue-500 text-white px-4 py-2 rounded hover:bg-blue-600"
        >
          End Turn
        </button>
      </div>
      
      <div className="grid gap-0 w-fit" style={{ gridTemplateColumns: `repeat(${GRID_SIZE}, 1fr)` }}>
      {renderGrid()}
      </div>
    </div>
  );
};

export default Game;