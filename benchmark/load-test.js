import http from 'k6/http';
import { SharedArray } from 'k6/data';

const payloads = new SharedArray('payloads', function () {
    return JSON.parse(open('./example-payloads.json'));
});

export const options = {
    vus: 10,
    duration: '10s',
};

const params = {
    headers: {
        'Content-Type': 'application/json',
    },
};

export default function () {
    const base =
        payloads[Math.floor(Math.random() * payloads.length)];

    const payload = JSON.stringify({
        ...base,
        id: `tx-${__VU}-${__ITER}`,
    });

    http.post(
        'http://transaction-processor-1:8080/fraud-score',
        payload,
        params
    );
}

export function handleSummary(data) {
    const metrics = data.metrics || {};
    const now = new Date()
        .toISOString()
        .replace(/[:.]/g, '-');

    return {
        [`/benchmark/results-${now}.json`]: JSON.stringify({
            p95:
                metrics.http_req_duration?.values?.['p(95)'] ?? 0,
            avg:
                metrics.http_req_duration?.values?.avg ?? 0,
            rps:
                metrics.http_reqs?.values?.rate ?? 0,
        }, null, 2),
    };
}

// RUNNING docker run --rm -i -v "${PWD}:/benchmark" --network infra_default grafana/k6 run /benchmark/load-test.js