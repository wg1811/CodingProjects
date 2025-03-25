//this is my model (class) file for my REST-API

//MVC model,view, controller

// first i will create a DRINK CLASS

class Drink {
  constructor(id, name) {
    this.id = id;
    this.name = name;
  }

  showDrink() {
    return `Drink Id: ${this.id}, Name: ${this.name}`;
  }
}

//inorder to add ,delete, update, show drinks i need another class
//[beer,soda,water,pepsi,whisky ]
//drinks class
class Drinks {
  constructor() {
    //my drinks will be stored in an ARRAY
    this.allDrinks = []; //empty
  }

  //we need some methods to add,delete,update, show drinks
  //ADD DRINK
  addDrink(drink) {
    //in our case drink cannot be anything but
    // it must be instance of DRINK class
    if (!(drink instanceof Drink)) {
      throw new Error("you must add drink instances only");
    }
    //we need to check if we add same drink second time
    //for example you added pepsi, later you also want to add pepsi
    // KOLA , kola ,Kola == kola EMILIE Emilie
    const exists = this.allDrinks.some(
      (d) => d.name.toLowerCase() === drink.name.toLowerCase()
    );
    //if this drink already exists exists=true if does not exist exists=false

    if (exists) {
      return {
        success: false,
        message: `The drink ${drink.name} is already added`,
      };
    } else {
      this.allDrinks.push(drink);
      return {
        success: true,
        message: `The drink ${drink.name} added `,
      };
    }
  }
  //i need a method to show all drinks
  showAllDrinks() {
    //if the list is empty
    if (this.allDrinks.length === 0) {
      return { success: false, message: "there are no drinks yet" };
    }
    // if there are some drinks
    return {
      success: true,
      drinks: this.allDrinks.map((drink) => drink.showDrink()),
      //if you have ["cola","pepsi","beer"]
    };
  }

  //show a drink by id /drink/1

  showDrinkById(id) {
    //we need to check if this id exist or not
    //check mechanism
    // [kola,wine,beer]
    const drink = this.allDrinks.find((d) => d.id === id);
    //if this drink does not exist
    if (!drink) {
      return { success: false, message: `Drink with id ${id} not found` };
    }
    //if this drink exists
    //we will return this drink
    return { success: true, drink: drink.showDrink() };
  }
  //delete an existed drink
  deleteDrink(id) {
    //i need to get index number to see if this drink existed or not
    const index = this.allDrinks.findIndex((d) => d.id === id);
    //if it gives me -1 which mean not found
    if (index === -1) {
      return { success: false, message: `Drink with id ${id} not found` };
    }
    const removeDrink = this.allDrinks.splice(index, 1)[0];
    //it means scan all drinks find index and simply remove the first element of this index
    return {
      success: true,
      message: `The drink ${removeDrink.name} deleted`,
    };
  }
  updateDrink(id, newName) {
    //we need to check this new name is a string or not
    if (!newName || typeof newName !== "string") {
      return { success: false, message: "Provide a string for new name" };
    }
    //if this drink not exist
    const drink = this.allDrinks.find((d) => d.id === id);
    //if it does not exist
    if (!drink) {
      return { success: false, message: `Drink with id ${id} not found` };
    }
    //okay if exists
    drink.name = newName;
    return {
      success: true,
      message: `the drink with id ${id} updated to ${drink.name}`,
    };
  }
}

//export drink and drinks
// in common js , node we export modules
module.exports = { Drink, Drinks };
