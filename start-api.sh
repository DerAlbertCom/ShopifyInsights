#!/bin/sh

export Logging__LogLevel__Default=Warning
export Logging__LogLevel__Microsoft=Warning
export Logging__LogLevel__System=Warning
export Logging__LogLevel__HistoDb=Information

export ASPNETCORE_ENVIRONMENT=Development

dotnet run -p src/ShopInsights.Web
