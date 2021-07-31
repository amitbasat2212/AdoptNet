
const data = [
    { month: 'January', DogsAdoptipon: 20 },
    { month: 'February', DogsAdoptipon: 32 },
    { month: 'March', DogsAdoptipon: 49 },
    { month: 'April', DogsAdoptipon: 21 },
    { month: 'May', DogsAdoptipon:70 },
    { month: 'June', DogsAdoptipon: 55 },
    { month: 'July', DogsAdoptipon: 90 },
    { month: 'August', DogsAdoptipon: 86 },
    { month: 'September', DogsAdoptipon: 79 },
    { month: 'October', DogsAdoptipon: 60 },
    { month: 'November', DogsAdoptipon: 94 },
    { month: 'December', DogsAdoptipon: 95 },
];


const width = 1200;
const height = 450;
const margin = { top: 50, bottom: 50, left: 0, right:0 };

const svg = d3.select('#d3-container')
    .append('svg')
    .attr('width', width - margin.left - margin.right)
    .attr('height', height - margin.top - margin.bottom)
    .attr("viewBox", [0, 0, width, height]);

const x = d3.scaleBand()
    .domain(d3.range(data.length))
    .range([margin.left, width - margin.right])
    .padding(0.1)

const y = d3.scaleLinear()
    .domain([0, 100])
    .range([height - margin.bottom, margin.top])

svg
    .append("g")
    .attr("fill", 'royalblue')
    .selectAll("rect")
    .data(data.sort((a, b) => d3.descending(1,12)))
    .join("rect")
    .attr("x", (d, i) => x(i))
    .attr("y", d => y(d.DogsAdoptipon))
    .attr('title', (d) => d.DogsAdoptipon)
    .attr("class", "rect")
    .attr("height", d => y(0) - y(d.DogsAdoptipon))
    .attr("width", x.bandwidth());

function yAxis(g) {
    g.attr("transform", `translate(${margin.left}, 0)`)
        .call(d3.axisLeft(y).ticks(null, data.format))
        .attr("font-size", '20px')
}

function xAxis(g) {
    g.attr("transform", `translate(0,${height - margin.bottom})`)
        .call(d3.axisBottom(x).tickFormat(i => data[i].month))
        .attr("font-size", '20px')
}

svg.append("g").call(xAxis);
svg.append("g").call(yAxis);
svg.node();








/*


*/










