FROM microsoft/dotnet:2.1-sdk as compiler
COPY . /root/src
RUN dotnet publish -c Release -v Detailed -o /root/publishedApp /root/src

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1 as base
COPY --from=compiler /root/publishedApp /root/publishedApp
WORKDIR /root/publishedApp
CMD ["dotnet", "pavlovLab.dll"]