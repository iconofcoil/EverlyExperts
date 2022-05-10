# EverlyExperts
Everly Health Experts directory search tool

Web application to add Experts (Members) and friends (Members) to each Member (bidirectional friendship.
Every time you add a Member, a Name and a Shortened Url to the website of the member is required. Just before record insertion,
the app gets the long url and then scrapes the website to get the text contained within the h1 to h3 headings. This is achieved by using
the HTML Agility Pack library.
For a particular Member, it is possible to enter a topic to search for. The web app then must return a list of "paths" or friend links which
website headings contain the topic being searched. This is achieved by using a recursive DFS algorithm, which returns a list of stacks of the
friend paths found. Then, before sending the result back, the content of the stacks is poured into a list, to get the proper order.
