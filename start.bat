@echo off
echo Iniciando Locadora.Cliente...
start cmd /k "cd Locadora.Cliente && dotnet run --project Locadora.Cliente"

echo Iniciando Locadora.Aluguel...
start cmd /k "cd Locadora.Aluguel && dotnet run --project Locadora.Aluguel"

echo Iniciando Locadora.Veiculo...
start cmd /k "cd Locadora.Veiculo && dotnet run --project Locadora.Veiculo"

echo Todas as aplicações foram iniciadas em terminais separados.
