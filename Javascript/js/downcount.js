'use strict';

var IntegerSet = function IntegerSet(numbers) {
    this.Numbers = []; 
}

IntegerSet.prototype.FromString = function (numbers) {
    if (typeof numbers !== 'string') {
        throw "IntegerSet: numbers not a string"
    }
    
    var res = numbers.split(",");
    for (var i = 0; i < res.length; ++i) {
        var number = parseInt(res[i]);
        if (isNaN(number)) {
            throw "Invalid number creating IntegerSet"
        }
        this.Numbers.push(number)
    }
}

IntegerSet.prototype.Add = function (x) {
    if (typeof x !== 'number' && Math.floor(x) !== x) {
        throw "Trying to add a non number to the IntegerSet"
    }

    this.Numbers.push(x);
};

IntegerSet.prototype.SubsetNotIncluding = function (n) {
    
    if (typeof n !== 'number' && Math.floor(n) !== n) {
        throw "SubsetNotIncluding: n not an integer"
    }
    
    if (n < 0 || n > this.Numbers.length - 1) {
        throw "SubsetNotIncluding: n out of range"
    }


    var subset = new IntegerSet();
    for (var i = 0; i < this.Numbers.length; ++i) {
        if (i != n) {
            subset.Add(this.Numbers[i]);
        }
    }

    return subset;
}

var DownCountGame = function DownCountGame() {
    this.TargetNumber;   
    this.SourceNumbers;
}

DownCountGame.prototype.Init = function(targetNumber, sourceNumbers) {
    
    if (typeof targetNumber !== 'number' || Math.floor(targetNumber) !== targetNumber) {
        throw "DownCountGame: targetNumber not an integer"
    }
    
    this.TargetNumber = targetNumber;
    
    this.SourceNumbers = new IntegerSet();
    this.SourceNumbers.FromString(sourceNumbers);
}

var AddOperator = function AddOperator(lhs, rhs)
{
    //if((typeof lhs != 'AddOperator' && typeof lhs != 'MinusOperator' && typeof lhs != 'MultiplyOperator' && typeof lhs != 'DivideOperator' && typeof lhs != 'number') ||
    //    (typeof rhs != 'AddOperator' && typeof rhs != 'MinusOperator' && typeof rhs != 'MultiplyOperator' && typeof rhs != 'DivideOperator' && typeof rhs != 'number')) {
    //        throw "Not a valid Operator"
    //}

    this.LHS = lhs;
    this.RHS = rhs;
    this.Symbol = '+';
}

AddOperator.prototype.Value = function () {
    var valLHS;
    var valRHS;
    if (typeof this.LHS == 'number') {
        valLHS = this.LHS;
    } else {
        valLHS = this.LHS.Value();
    }
    
    if (typeof this.RHS == 'number') {
        valRHS = this.RHS;
    } else {
        valRHS = this.RHS.Value();
    }

    return valLHS + valRHS;
}

AddOperator.prototype.toString = function () {  
    return '(' + this.LHS.toString() + this.Symbol + this.RHS.toString() + ')';
}

var MinusOperator = function MinusOperator(lhs, rhs) {

    //if ((typeof lhs != 'AddOperator' && typeof lhs != 'MinusOperator' && typeof lhs != 'MultiplyOperator' && typeof lhs != 'DivideOperator' && typeof lhs != 'number') ||
    //    (typeof rhs != 'AddOperator' && typeof rhs != 'MinusOperator' && typeof rhs != 'MultiplyOperator' && typeof rhs != 'DivideOperator' && typeof rhs != 'number')) {
    //    throw "Not a valid Operator"
    //}

    this.LHS = lhs;
    this.RHS = rhs;
    this.Symbol = '-';
}

MinusOperator.prototype.Value = function () {
    var valLHS;
    var valRHS;
    if (typeof this.LHS == 'number') {
        valLHS = this.LHS;
    } else {
        valLHS = this.LHS.Value();
    }
    
    if (typeof this.RHS == 'number') {
        valRHS = this.RHS;
    } else {
        valRHS = this.RHS.Value();
    }
    
    return valLHS - valRHS;
}

MinusOperator.prototype.toString = function () {
    return '(' + this.LHS.toString() + this.Symbol + this.RHS.toString() + ')';
}

var MultiplyOperator = function MultiplyOperator(lhs, rhs) {

    //if ((typeof lhs != 'AddOperator' && typeof lhs != 'MinusOperator' && typeof lhs != 'MultiplyOperator' && typeof lhs != 'DivideOperator' && typeof lhs != 'number') ||
    //    (typeof rhs != 'AddOperator' && typeof rhs != 'MinusOperator' && typeof rhs != 'MultiplyOperator' && typeof rhs != 'DivideOperator' && typeof rhs != 'number')) {
    //    throw "Not a valid Operator"
    //}

    this.LHS = lhs;
    this.RHS = rhs;
    this.Symbol = 'x';
}

MultiplyOperator.prototype.Value = function () {
    var valLHS;
    var valRHS;
    if (typeof this.LHS == 'number') {
        valLHS = this.LHS;
    } else {
        valLHS = this.LHS.Value();
    }
    
    if (typeof this.RHS == 'number') {
        valRHS = this.RHS;
    } else {
        valRHS = this.RHS.Value();
    }
    
    return valLHS * valRHS;
}

MultiplyOperator.prototype.toString = function () {
    return '(' + this.LHS.toString() + this.Symbol + this.RHS.toString() + ')';
}

var DivideOperator = function DivideOperator(lhs, rhs) {
    
    //if ((typeof lhs != 'AddOperator' && typeof lhs != 'MinusOperator' && typeof lhs != 'MultiplyOperator' && typeof lhs != 'DivideOperator' && typeof lhs != 'number') ||
    //    (typeof rhs != 'AddOperator' && typeof rhs != 'MinusOperator' && typeof rhs != 'MultiplyOperator' && typeof rhs != 'DivideOperator' && typeof rhs != 'number')) {
    //    throw "Not a valid Operator"
    //}
    this.LHS = lhs;
    this.RHS = rhs;
    this.Symbol = '/';
}

DivideOperator.prototype.Value = function () {
    var valLHS;
    var valRHS;
    if (typeof this.LHS == 'number') {
        valLHS = this.LHS;
    } else {
        valLHS = this.LHS.Value();
    }
    
    if (typeof this.RHS == 'number') {
        valRHS = this.RHS;
    } else {
        valRHS = this.RHS.Value();
    }
    
    return valLHS / valRHS;
}

DivideOperator.prototype.toString = function () {
    return '(' + this.LHS.toString() + this.Symbol + this.RHS.toString() + ')';
}

var Solutions = function Solutions() {
    this.Equations = [];
}

Solutions.prototype.Add = function(equation) {
    for (var i= 0; i < this.Equations.length; ++i) {
        this.Equations[i] = new AddOperator(this.Equations[i], equation);
    }
}

Solutions.prototype.Minus = function (equation) {
    for (var i = 0; i < this.Equations.length; ++i) {
        this.Equations[i] = new MinusOperator(this.Equations[i], equation);
    }
}

Solutions.prototype.Multiply = function (equation) {
    for (var i = 0; i < this.Equations.length; ++i) {
        this.Equations[i] = new MultiplyOperator(this.Equations[i], equation);
    }
}

Solutions.prototype.Divide = function (equation) {
    for (var i = 0; i < this.Equations.length; ++i) {
        if (equation.Value !== 0) {
            this.Equations[i] = new DivideOperator(this.Equations[i], equation);
        }
    }
}

var DownCountSolver = function DownCountSolver(){
    
    this.Game;

}

DownCountSolver.prototype.Solve = function (targetNumber, sourceNumbers, allSolutions) {
    
    if (typeof allSolutions !== 'boolean' ) {
        throw "Invalid allSolutions";
    }
    
    var game = new DownCountGame();
    game.Init(targetNumber, sourceNumbers);

    return this.SolveGame(game, allSolutions);
};

DownCountSolver.prototype.SolveGame = function (game, allSolutions) {
    
    if (typeof allSolutions !== 'boolean') {
        throw "Invalid allSolutions";
    }
    
    var solutions = new Solutions();

    if (game.SourceNumbers.Numbers.indexOf(game.TargetNumber) >= 0) {
        solutions.Equations.push(game.TargetNumber);
    }
    
    if (!allSolutions && solutions.Equations.length > 0) {
        return solutions;
    }
    
    
    if (game.SourceNumbers.Numbers.length == 0) {
        return solutions;
    }
    
    for (var i = 0; i < game.SourceNumbers.Numbers.length; ++i) {
        var solutions = this.SolveSubset(game, i, allSolutions)
        
        if (!allSolutions && solutions.Equations.length > 0)
            return solutions;
    }
    
    return solutions;
}

DownCountSolver.prototype.SolveSubset = function (game, i, allSolutions) {

    var solutions = new Solutions();
    var currentNumber = game.SourceNumbers.Numbers[i];
    var subset = game.SourceNumbers.SubsetNotIncluding(i);

    var gameAdd = new DownCountGame();
    gameAdd.TargetNumber = game.TargetNumber - currentNumber;
    gameAdd.SourceNumbers = subset;
    var solutionsAdd = this.SolveGame(gameAdd, allSolutions);
    solutionsAdd.Add(currentNumber);
    for (var i = 0; i < solutionsAdd.Equations.length; ++i) {
        solutions.Equations.push(solutionsAdd.Equations[i])
    }
    if (!allSolutions && solutions.Equations.length > 0) {
        return solutions;
    }

    var gameMinus = new DownCountGame();
    gameMinus.TargetNumber = game.TargetNumber + currentNumber;
    gameMinus.SourceNumbers = subset;
    var solutionsMinus = this.SolveGame(gameMinus, allSolutions);
    solutionsMinus.Minus(currentNumber);
    for (var i = 0; i < solutionsMinus.Equations.length; ++i) {
        solutions.Equations.push(solutionsMinus.Equations[i])
    }
    if (!allSolutions && solutions.Equations.length > 0) {
        return solutions;
    }

    var gameMultiply = new DownCountGame();
    if (game.TargetNumber % currentNumber == 0) {
        gameMultiply.TargetNumber = game.TargetNumber / currentNumber;
        gameMultiply.SourceNumbers = subset;
        var solutionsMultiply = this.SolveGame(gameMultiply, allSolutions);
        solutionsMultiply.Multiply(currentNumber);
        for (var i = 0; i < solutionsMultiply.Equations.length; ++i) {
            solutions.Equations.push(solutionsMultiply.Equations[i])
        }
        if (!allSolutions && solutions.Equations.length > 0) {
            return solutions;
        }
    }

    var gameDivide = new DownCountGame();
    gameDivide.TargetNumber = game.TargetNumber * currentNumber;
    gameDivide.SourceNumbers = subset;
    var solutionsDivide = this.SolveGame(gameDivide, allSolutions);
    solutionsDivide.Divide(currentNumber);
    for (var i = 0; i < solutionsDivide.Equations.length; ++i) {
        solutions.Equations.push(solutionsDivide.Equations[i])
    }
    if (!allSolutions && solutions.Equations.length > 0) {
        return solutions;
    }

    return solutions;
}

module.exports.DownCountSolver = DownCountSolver;