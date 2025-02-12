import React, { useState, useEffect } from 'react';

// Basic types
type Position = { x: number; y: number };
type UnitType = 'warrior' | 'archer' | 'mage';

interface Unit {
  id: string;
  type: UnitType;
  position: Position;
  health: number;
  movement: number;
  hasMoved: boolean;
}

interface GameState {
  units: Unit[];
  selectedUnit: Unit | null;
  turn: number;
  phase: 'movement' | 'action';
}

const Game = () => {
  // Initial game state
  const [gameState, setGameState] = useState<GameState>({
    units: [
      { id: '1', type: 'warrior', position: { x: 1, y: 1 }, health: 100, movement: 2, hasMoved: false },
      { id: '2', type: 'archer', position: { x: 2, y: 1 }, health: 80, movement: 3, hasMoved: false },
    ],
    selectedUnit: null,
    turn: 1,
    phase: 'movement'
  });

  // Grid size
  const gridSize = 8;

  // Generate game grid
  const renderGrid = () => {
    const grid = [];
    for (let y = 0; y < gridSize; y++) {
      for (let x = 0; x < gridSize; x++) {
        const unit = gameState.units.find(u => u.position.x === x && u.position.y === y);
        grid.push(
          <div
            key={`${x}-${y}`}
            className={`w-16 h-16 border border-gray-300 flex items-center justify-center
              ${unit ? 'bg-blue-200' : 'bg-white'}
              ${gameState.selectedUnit?.position.x === x && gameState.selectedUnit?.position.y === y ? 'bg-blue-400' : ''}
            `}
            onClick={() => handleCellClick(x, y)}
          >
            {unit && (
              <div className="text-sm">
                {unit.type}
                <div className="text-xs">{unit.health}HP</div>
              </div>
            )}
          </div>
        );
      }
    }
    return grid;
  };

  // Handle clicking on a cell
  const handleCellClick = (x: number, y: number) => {
    const clickedUnit = gameState.units.find(u => u.position.x === x && u.position.y === y);
    
    // If we already have a selected unit, try to move it
    if (gameState.selectedUnit && !clickedUnit) {
      if (isValidMove(gameState.selectedUnit, { x, y })) {
        moveUnit(gameState.selectedUnit, { x, y });
      }
    } 
    // Otherwise, select the clicked unit if there is one
    else if (clickedUnit && !clickedUnit.hasMoved) {
      setGameState(prev => ({ ...prev, selectedUnit: clickedUnit }));
    }
  };

  // Check if a move is valid
  const isValidMove = (unit: Unit, newPos: Position): boolean => {
    const distance = Math.abs(unit.position.x - newPos.x) + Math.abs(unit.position.y - newPos.y);
    return distance <= unit.movement && !unit.hasMoved;
  };

  // Move a unit
  const moveUnit = (unit: Unit, newPos: Position) => {
    setGameState(prev => ({
      ...prev,
      units: prev.units.map(u => 
        u.id === unit.id 
          ? { ...u, position: newPos, hasMoved: true }
          : u
      ),
      selectedUnit: null
    }));
  };

  // End turn
  const endTurn = () => {
    setGameState(prev => ({
      ...prev,
      turn: prev.turn + 1,
      units: prev.units.map(u => ({ ...u, hasMoved: false })),
      selectedUnit: null
    }));
  };

  return (
    <div className="p-4">
      <div className="mb-4">
        <h2 className="text-xl font-bold">Turn {gameState.turn}</h2>
        <button 
          onClick={endTurn}
          className="mt-2 bg-blue-500 text-white px-4 py-2 rounded"
        >
          End Turn
        </button>
      </div>
      <div className="grid grid-cols-8 gap-0 w-fit">
        {renderGrid()}
      </div>
    </div>
  );
};

export default Game;