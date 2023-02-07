
import './App.css';
import { connect } from 'react-redux'
import { incrementFun, decrementFun, apiFun } from './Redux/action';
import MainComponent from './Components/MainComponent';


function App(props) {
  return (
    <MainComponent/>
  );
}

























function mapStateToProps(state) {
  return {
    state
  }
}
function mapDispatchToProps(dispatch) {
  return {
    inc: () => dispatch(incrementFun()),
    dec: () => dispatch(decrementFun()),
    api: () => dispatch(apiFun())
  }
}
export default connect(mapStateToProps, mapDispatchToProps)(App);
