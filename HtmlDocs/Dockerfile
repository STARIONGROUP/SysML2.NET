FROM nginx:alpine

WORKDIR /usr/share/nginx/html

COPY HtmlDocs/nginx.conf /etc/nginx/nginx.conf

COPY ../SysML2.NET.CodeGenerator.Tests/bin/Debug/net7.0/AutoGenHtmlDocs/index.html  .
COPY ../SysML2.NET.CodeGenerator.Tests/bin/Debug/net7.0/AutoGenHtmlDocs/sysml2-class-inheritance.svg  .
COPY ../SysML2.NET.CodeGenerator/Resources/HtmlDocs .