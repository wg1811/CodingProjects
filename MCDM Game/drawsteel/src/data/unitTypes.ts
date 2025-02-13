export type Ability = {
    name: string;
    description: string;
    keywords: ("AREA" | "CHARGE" | "MAGIC" | "MELEE" | "PSIONIC" | "RANGED" | "STRIKE" | "WEAPON")[];
  };
  
  export type UnitType = {
    name: string;
    hitpoints: number;
    class: "warrior" | "archer" | "mage" | "rogue"; // Add more as needed
    victories: number;
    inventory: string[]; // List of item names
    weapon: string;
    abilities: Ability[];
    movementRange: number;
  };
  
  export const UNIT_TYPES: Record<string, UnitType> = {
    //  e.g.
    // warrior: {
    //   name: "Warrior",
    //   hitpoints: 100,
    //   class: "warrior",
    //   victories: 0,
    //   inventory: ["Health Potion", "Shield"],
    //   weapon: "Sword",
    //   abilities: [
    //     {
    //       name: "Power Strike",
    //       description: "A heavy melee attack that deals extra damage.",
    //       keywords: ["MELEE", "STRIKE", "WEAPON"],
    //     },
    //     {
    //       name: "Battle Cry",
    //       description: "Increases nearby allies' attack power.",
    //       keywords: ["AREA"],
    //     },
    //   ],
    //   movementRange: 2,
    // },
    // archer: {
    //   name: "Archer",
    //   hitpoints: 80,
    //   class: "archer",
    //   victories: 0,
    //   inventory: ["Arrows", "Dagger"],
    //   weapon: "Bow",
    //   abilities: [
    //     {
    //       name: "Piercing Shot",
    //       description: "A ranged attack that ignores armor.",
    //       keywords: ["RANGED", "STRIKE", "WEAPON"],
    //     },
    //     {
    //       name: "Eagle Eye",
    //       description: "Increases accuracy for a short time.",
    //       keywords: [],
    //     },
    //   ],
    //   movementRange: 3,
    // },
  };
  