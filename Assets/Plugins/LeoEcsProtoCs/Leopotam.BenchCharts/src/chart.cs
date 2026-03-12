// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Leopotam.BenchCharts {
    public class BenchCharts {
        string _template = @"
<!DOCTYPE html><html lang='ru'><head><meta charset='UTF-8'>
<meta name='viewport' content='width=device-width,initial-scale=1.0'>
<script src='https://cdn.jsdelivr.net/npm/chart.js@4.4.0/dist/chart.umd.min.js'></script>
</head><body/><script>function addChart(c){const el=document.createElement('canvas');
el.style.display='inline';document.body.appendChild(el);new Chart(el,{type:'line',options:{
scales:{x:{title:{display:true,text:c.labels.xName,font:{style:'italic'}}},
y:{title:{display:true,text:c.labels.yName,font:{style:'italic'}}}},plugins:{
title:{display:true,text:c.title,font:{size:'18px',weight:'bold'}},
legend:{position:'bottom',labels:{usePointStyle:true,boxWidth:5,boxHeight:5}},
tooltip:{usePointStyle:true,footerColor:'red'}},},data:{labels: c.labels.xData,
datasets:c.data.map((dataItem)=>({label:dataItem.name,data:dataItem.data}))}})}
const userData=[];for(const d of userData){addChart(d)}</script></html>";

        List<BenchChart> _charts;

        public BenchCharts () {
            _charts = new ();
        }
        public BenchChart NewChart () {
            BenchChart chart = new ();
            _charts.Add (chart);
            return chart;
        }

        public string Build () {
            StringBuilder sb = new ();
            for (var i = 0; i < _charts.Count; i++) {
                if (i > 0) { sb.Append (","); }
                sb.Append (_charts[i].ToJs ());
            }

            return _template.Replace ("const userData=[]", $"const userData=[{sb}]");
        }

        internal static string JsString (string str) => str.Replace ("\'", "\\'");
    }

    public class BenchChart {
        string _title;
        string _yName;
        string _xName;
        List<string> _xMarks;
        List<BenchChartReport> _reports;

        internal BenchChart () {
            _title = "";
            _yName = "";
            _xName = "";
            _xMarks = new ();
            _reports = new ();
        }

        public BenchChart SetTitle (string title) {
            _title = title ?? "";
            return this;
        }

        public BenchChart SetAxes (string xName, string yName) {
            _xName = xName ?? "";
            _yName = yName ?? "";
            return this;
        }

        public BenchChart SetMarks (params string[] xMarks) {
            _reports.Clear ();
            _xMarks.Clear ();
            if (_xMarks != null) {
                _xMarks.AddRange (xMarks);
            }
            return this;
        }
        public BenchChartReport NewReport (string name) {
            BenchChartReport r = new (name, _xMarks.Count);
            _reports.Add (r);
            return r;
        }

        public string ToJs () {
            StringBuilder sb = new ();
            sb.Append ("{");
            sb.Append ($"title:'{BenchCharts.JsString (_title)}',");
            sb.Append ("labels:{");
            sb.Append ($"yName:'{BenchCharts.JsString (_yName)}',");
            sb.Append ($"xName:'{BenchCharts.JsString (_xName)}',");
            sb.Append ("xData:[");
            for (var i = 0; i < _xMarks.Count; i++) {
                if (i > 0) { sb.Append (","); }
                sb.Append ($"'{BenchCharts.JsString (_xMarks[i])}'");
            }
            sb.Append ("]},");
            sb.Append ("data:[");
            for (var i = 0; i < _reports.Count; i++) {
                if (i > 0) { sb.Append (","); }
                sb.Append (_reports[i].ToJs ());
            }
            sb.Append ("]}");
            return sb.ToString ();
        }

        public int TestsCount () => _xMarks.Count;
    }

    public class BenchChartReport {
        string _name;
        int _cap;
        List<double> _values;

        internal BenchChartReport (string name, int count) {
            _name = name ?? "";
            _cap = count;
            _values = new (_cap);
        }

        public BenchChartReport Add (double val) {
            if (_values.Count < _cap) {
                _values.Add (val);
            }
            return this;
        }

        public string ToJs () {
            StringBuilder sb = new ();
            sb.Append ($"{{name:'{BenchCharts.JsString (_name)}',data:[");
            for (var i = 0; i < _values.Count; i++) {
                if (i > 0) { sb.Append (","); }
                sb.Append (_values[i].ToString (NumberFormatInfo.InvariantInfo));
            }
            sb.Append ("]}");
            return sb.ToString ();
        }

        public int TestsCount () => _cap;
    }
}
