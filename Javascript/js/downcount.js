'use strict';

var IntegerSet = function IntegerSet(numbers) {
    this.Numbers = [];
    for (var i = 0; i < numbers.length; ++i) {
        this.Add(numbers[i]);
    }
};

IntegerSet.prototype.Add = function (x) {
    if (typeof x !== 'number' && Math.floor(x) !== x) {
        throw "Trying to add a non number to the IntegerSet";
    }

    this.Numbers.push(x);
};

IntegerSet.prototype.SubsetNotIncluding = function (n) {
    
    if (typeof n !== 'number' && Math.floor(n) !== n) {
        throw "SubsetNotIncluding: n not an integer";
    }
    
    if (n < 0 || n > this.Numbers.length - 1) {
        throw "SubsetNotIncluding: n out of range";
    }
    
    
    var subset = [];
    for (var i = 0; i < this.Numbers.length; ++i) {
        if (i != n) {
            subset.push(this.Numbers[i]);
        }
    }
    
    return new IntegerSet(subset);
};

var DownCountGame = function DownCountGame() {
    this.TargetNumber = 0;
    this.SourceNumbers = new IntegerSet([]);
};

DownCountGame.prototype.Init = function (targetNumber, sourceNumbers) {
    
    if (typeof targetNumber !== 'number' || Math.floor(targetNumber) !== targetNumber) {
        throw "DownCountGame: targetNumber not an integer";
    }
    
    this.TargetNumber = targetNumber;
    
    this.SourceNumbers = new IntegerSet(sourceNumbers);
};

var AddOperator = function AddOperator(lhs, rhs) {
    this.LHS = lhs;
    this.RHS = rhs;
    this.Symbol = '+';
};

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
};

AddOperator.prototype.toString = function () {
    return '(' + this.LHS.toString() + this.Symbol + this.RHS.toString() + ')';
};

var MinusOperator = function MinusOperator(lhs, rhs) {
    
    this.LHS = lhs;
    this.RHS = rhs;
    this.Symbol = '-';
};

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
};

MinusOperator.prototype.toString = function () {
    return '(' + this.LHS.toString() + this.Symbol + this.RHS.toString() + ')';
};

var MultiplyOperator = function MultiplyOperator(lhs, rhs) {
    
    this.LHS = lhs;
    this.RHS = rhs;
    this.Symbol = 'x';
};

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
};

MultiplyOperator.prototype.toString = function () {
    return '(' + this.LHS.toString() + this.Symbol + this.RHS.toString() + ')';
};

var DivideOperator = function DivideOperator(lhs, rhs) {
    
    this.LHS = lhs;
    this.RHS = rhs;
    this.Symbol = '/';
};

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
};

DivideOperator.prototype.toString = function () {
    return '(' + this.LHS.toString() + this.Symbol + this.RHS.toString() + ')';
};

var Solutions = function Solutions() {
    this.Equations = [];
};

Solutions.prototype.Add = function (equation) {
    for (var i = 0; i < this.Equations.length; ++i) {
        this.Equations[i] = new AddOperator(this.Equations[i], equation);
    }
};

Solutions.prototype.Minus = function (equation) {
    for (var i = 0; i < this.Equations.length; ++i) {
        this.Equations[i] = new MinusOperator(this.Equations[i], equation);
    }
};

Solutions.prototype.Multiply = function (equation) {
    for (var i = 0; i < this.Equations.length; ++i) {
        this.Equations[i] = new MultiplyOperator(this.Equations[i], equation);
    }
};

Solutions.prototype.Divide = function (equation) {
    for (var i = 0; i < this.Equations.length; ++i) {
        if (equation.Value !== 0) {
            this.Equations[i] = new DivideOperator(this.Equations[i], equation);
        }
    }
};

var DownCountSolver = function DownCountSolver() {    
};

DownCountSolver.prototype.Solve = function (targetNumber, sourceNumbers, closeSolutions, allSolutions) {
    
    if (typeof allSolutions !== 'boolean' ) {
        throw "allSolutions not a boolean";
    }
    
    if (typeof closeSolutions !== 'boolean') {
        throw "closeSolutions not a boolean";
    }
    
    if (typeof targetNumber !== 'number' || Math.floor(targetNumber) !== targetNumber) {
        throw "targetNumber not an integer";
    }
    
    var game = new DownCountGame();
    game.Init(targetNumber, sourceNumbers);

    var solutions = new Solutions();

    solutions = this.SolveGame(game, allSolutions);
    if (solutions.Equations.length > 0) {
        // We found some exact solutions so we can return now
        return solutions;
    }

    if (closeSolutions) {
        // Try to find some close solutions
        var originalTargetNumber = game.TargetNumber;
        for (var i = 1; i <= 10; ++i) {
            game.TargetNumber = originalTargetNumber + i;
            solutions = this.SolveGame(game, false);
            if (solutions.Equations.length > 0) {
                return solutions;
            }

            game.TargetNumber = originalTargetNumber - i;
            solutions = this.SolveGame(game, false);
            if (solutions.Equations.length > 0) {
                return solutions;
            }
        }
    }

    return solutions;
};

///
/// Return only exact solutions for the game
///
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
    
    
    if (game.SourceNumbers.Numbers.length === 0) {
        return solutions;
    }
    
    for (var i = 0; i < game.SourceNumbers.Numbers.length; ++i) {
        var solutionsSubset = this.SolveSubset(game, i, allSolutions);
        for (var j = 0; j < solutionsSubset.Equations.length; ++j) {
            solutions.Equations.push(solutionsSubset.Equations[j]);
        }
        
        if (!allSolutions && solutions.Equations.length > 0)
            return solutions;
    }
    
    return solutions;
};

///
/// Solve the nth subset of the given game
///
DownCountSolver.prototype.SolveSubset = function (game, n, allSolutions) {
    
    var solutions = new Solutions();
    var currentNumber = game.SourceNumbers.Numbers[n];
    var subset = game.SourceNumbers.SubsetNotIncluding(n);
    
    var gameAdd = new DownCountGame();
    gameAdd.TargetNumber = game.TargetNumber - currentNumber;
    gameAdd.SourceNumbers = subset;
    var solutionsAdd = this.SolveGame(gameAdd, allSolutions);
    solutionsAdd.Add(currentNumber);
    for (var i = 0; i < solutionsAdd.Equations.length; ++i) {
        solutions.Equations.push(solutionsAdd.Equations[i]);
    }
    if (!allSolutions && solutions.Equations.length > 0) {
        return solutions;
    }
    
    var gameMinus = new DownCountGame();
    gameMinus.TargetNumber = game.TargetNumber + currentNumber;
    gameMinus.SourceNumbers = subset;
    var solutionsMinus = this.SolveGame(gameMinus, allSolutions);
    solutionsMinus.Minus(currentNumber);
    for (i = 0; i < solutionsMinus.Equations.length; ++i) {
        solutions.Equations.push(solutionsMinus.Equations[i]);
    }
    if (!allSolutions && solutions.Equations.length > 0) {
        return solutions;
    }
    
    var gameMultiply = new DownCountGame();
    if (game.TargetNumber % currentNumber === 0) {
        gameMultiply.TargetNumber = game.TargetNumber / currentNumber;
        gameMultiply.SourceNumbers = subset;
        var solutionsMultiply = this.SolveGame(gameMultiply, allSolutions);
        solutionsMultiply.Multiply(currentNumber);
        for (i = 0; i < solutionsMultiply.Equations.length; ++i) {
            solutions.Equations.push(solutionsMultiply.Equations[i]);
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
    for (i = 0; i < solutionsDivide.Equations.length; ++i) {
        solutions.Equations.push(solutionsDivide.Equations[i]);
    }
    if (!allSolutions && solutions.Equations.length > 0) {
        return solutions;
    }
    
    return solutions;
};

module.exports.DownCountSolver = DownCountSolver;